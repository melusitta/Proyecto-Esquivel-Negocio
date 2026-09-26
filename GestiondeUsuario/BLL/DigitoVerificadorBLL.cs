using DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class DigitoVerificadorBLL
    {
        private static DigitoVerificadorBLL _instancia;
        private DigitoVerificadorBLL() { }

        public static DigitoVerificadorBLL Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new DigitoVerificadorBLL();
                return _instancia;
            }
        }

        // Convierte un string a su suma de valores hexadecimales
        private int ConvertirAHex(string valor)
        {
            int suma = 0;
            foreach (char c in valor)
                suma += (int)c;
            return suma;
        }

        // Calcula DVH (suma horizontal de todos los campos de un registro)
        // y DVV (suma vertical de todos los registros por columna)
        // Devuelve (dvhTotal, dvvTotal) como strings
        private (string dvh, string dvv) Calcular(List<List<string>> filas)
        {
            if (filas == null || filas.Count == 0)
                return ("0", "0");

            int columnas = filas[0].Count;
            int[] sumaColumnas = new int[columnas];
            int sumaFilas = 0;

            foreach (var fila in filas)
            {
                int sumaFila = 0;
                for (int i = 0; i < fila.Count; i++)
                {
                    int val = ConvertirAHex(fila[i]);
                    sumaFila += val;
                    sumaColumnas[i] += val;
                }
                sumaFilas += sumaFila;
            }

            // DVH = suma de todas las sumas de fila
            string dvh = sumaFilas.ToString();

            // DVV = suma de todas las sumas de columna
            int totalColumnas = 0;
            foreach (var s in sumaColumnas)
                totalColumnas += s;
            string dvv = totalColumnas.ToString();

            return (dvh, dvv);
        }

        // Tablas protegidas por el dígito verificador y sus datos actuales.
        // Para sumar una tabla nueva alcanza con agregarla acá.
        private Dictionary<string, List<List<string>>> ObtenerTablas(DigitoVerificadorDAL dal)
        {
            return new Dictionary<string, List<List<string>>>
            {
                { "Usuarios",    dal.ObtenerDatosUsuarios() },
                { "Rol",         dal.ObtenerDatosRol() },
                { "Patente",     dal.ObtenerDatosPatente() },
                { "Familia",     dal.ObtenerDatosFamilia() },
                // Negocio (PN1)
                { "Producto",    dal.ObtenerDatosProducto() },
                { "Cliente",     dal.ObtenerDatosCliente() },
                { "Carrito",     dal.ObtenerDatosCarrito() },
                { "ItemCarrito", dal.ObtenerDatosItemCarrito() },
                { "Factura",     dal.ObtenerDatosFactura() },
                { "ItemFactura", dal.ObtenerDatosItemFactura() }
            };
        }

        // Recalcula y persiste el DV de todas las tablas
        public void RecalcularYGuardar()
        {
            var dal = new DigitoVerificadorDAL();

            foreach (var kvp in ObtenerTablas(dal))
            {
                var (dvh, dvv) = Calcular(kvp.Value);
                dal.GuardarDV(kvp.Key, dvh, dvv);
            }
        }

        // Verifica si los DV calculados coinciden con los guardados
        // Devuelve true si todo está bien, false si hay inconsistencia
        public bool Verificar()
        {
            var dal = new DigitoVerificadorDAL();

            foreach (var kvp in ObtenerTablas(dal))
            {
                var guardado = dal.ObtenerDV(kvp.Key);
                if (guardado == null) return false;

                var (dvhCalc, dvvCalc) = Calcular(kvp.Value);

                if (guardado.DVH != dvhCalc || guardado.DVV != dvvCalc)
                    return false;
            }

            return true;
        }

        public void InicializarSiEsNecesario()
        {
            var dal = new DigitoVerificadorDAL();
            var dv = dal.ObtenerDV("Usuarios");

            // Si el DVH es "0" significa que nunca se calculó
            if (dv == null || dv.DVH == "0")
                RecalcularYGuardar();
        }
    }
}