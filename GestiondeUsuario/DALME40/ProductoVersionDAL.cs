﻿using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    // Bitácora de cambios del maestro de productos: lee las versiones de la tabla ProductoVersion.
    // Las versiones las guarda el trigger trg_Producto_Audit (ver ProductoDAL.EjecutarConAuditoria); acá solo se leen,
    // como DataTable: no hace falta una clase para cada versión.
    public class ProductoVersionDAL
    {
        private string connectionString = ConexionDAL.ConnectionString;

        // Cada versión con los datos de la anterior del mismo producto (columnas Ant_*), para mostrar qué cambió
        private const string Consulta = @"SELECT v.Id AS IdVersion, v.NroVersion, v.TipoCambio, v.Fecha, v.Usuario, v.Detalle, v.VersionActiva,
                v.IdProducto AS Id, v.CodigoProducto, v.Nombre, v.Marca, v.Color, v.Modelo, v.PrecioUnitario, v.Existencia,
                v.Activo, v.Categoria, v.StockMinimo, v.IdProveedorHabitual, pr.RazonSocial AS ProveedorHabitual,
                a.Id AS Ant_Id, a.CodigoProducto AS Ant_CodigoProducto, a.Nombre AS Ant_Nombre, a.Marca AS Ant_Marca,
                a.Color AS Ant_Color, a.Modelo AS Ant_Modelo, a.PrecioUnitario AS Ant_PrecioUnitario,
                a.Existencia AS Ant_Existencia, a.Activo AS Ant_Activo, a.Categoria AS Ant_Categoria,
                a.StockMinimo AS Ant_StockMinimo, a.IdProveedorHabitual AS Ant_IdProveedorHabitual,
                pa.RazonSocial AS Ant_ProveedorHabitual
            FROM ProductoVersion v
            LEFT JOIN Proveedor pr ON pr.Id = v.IdProveedorHabitual
            LEFT JOIN ProductoVersion a ON a.IdProducto = v.IdProducto AND a.NroVersion = v.NroVersion - 1
            LEFT JOIN Proveedor pa ON pa.Id = a.IdProveedorHabitual";

        // Una versión (null si no existe)
        public DataRow ObtenerPorId(int idVersion)
        {
            DataTable tabla = Leer(Consulta + " WHERE v.Id = @Valor", cmd => cmd.Parameters.AddWithValue("@Valor", idVersion));
            return tabla.Rows.Count > 0 ? tabla.Rows[0] : null;
        }

        // Cambios de todos los productos entre dos fechas (incluidas), de los más nuevos a los más viejos.
        // texto: código exacto, o parte del nombre o la marca. usuario y tipoCambio: null = todos.
        public DataTable ObtenerCambios(DateTime desde, DateTime hasta, string texto, string usuario, string tipoCambio)
        {
            string query = Consulta + @"
                WHERE v.Fecha >= @Desde AND v.Fecha < DATEADD(DAY, 1, @Hasta)
                  AND (@Texto IS NULL OR v.Nombre LIKE @Like OR v.Marca LIKE @Like OR CAST(v.CodigoProducto AS NVARCHAR(20)) = @Texto)
                  AND (@Usuario IS NULL OR v.Usuario = @Usuario)
                  AND (@Tipo IS NULL OR v.TipoCambio = @Tipo)
                ORDER BY v.Fecha DESC, v.NroVersion DESC";
            return Leer(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@Desde", desde.Date);
                cmd.Parameters.AddWithValue("@Hasta", hasta.Date);
                cmd.Parameters.AddWithValue("@Texto", (object)texto ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Like", texto == null ? (object)DBNull.Value : "%" + texto + "%");
                cmd.Parameters.AddWithValue("@Usuario", (object)usuario ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Tipo", (object)tipoCambio ?? DBNull.Value);
            });
        }

        // Usuarios que hicieron algún cambio (para el filtro)
        public List<string> ObtenerUsuarios()
        {
            var lista = new List<string>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT Usuario FROM ProductoVersion ORDER BY Usuario", con);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                    while (reader.Read())
                        lista.Add(reader["Usuario"].ToString());
            }
            return lista;
        }

        private DataTable Leer(string query, Action<SqlCommand> parametros)
        {
            DataTable tabla = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                parametros(cmd);
                new SqlDataAdapter(cmd).Fill(tabla);
            }
            return tabla;
        }

        // Los datos del producto guardados en una versión (para restaurarla)
        public static Producto ProductoDeVersion(DataRow v)
        {
            return new Producto
            {
                Id = Convert.ToInt32(v["Id"]),
                CodigoProducto = Convert.ToInt32(v["CodigoProducto"]),
                Nombre = v["Nombre"].ToString(),
                Marca = v["Marca"].ToString(),
                Color = v["Color"].ToString(),
                Modelo = v["Modelo"].ToString(),
                PrecioUnitario = Convert.ToDecimal(v["PrecioUnitario"]),
                Existencia = Convert.ToInt32(v["Existencia"]),
                Activo = Convert.ToBoolean(v["Activo"]),
                Categoria = v["Categoria"].ToString(),
                StockMinimo = Convert.ToInt32(v["StockMinimo"]),
                IdProveedorHabitual = v["IdProveedorHabitual"] == DBNull.Value ? (int?)null : Convert.ToInt32(v["IdProveedorHabitual"]),
                ProveedorHabitual = v["ProveedorHabitual"] == DBNull.Value ? null : v["ProveedorHabitual"].ToString()
            };
        }
    }
}
