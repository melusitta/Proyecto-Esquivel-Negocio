using BE;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    // PN2: recepción de la mercadería en el depósito (CUN-011 Registrar productos recibidos)
    public class RecepcionDAL
    {
        private string connectionString = ConexionDAL.ConnectionString;

        // Registra la recepción en una transacción: la orden pasa de Pagada a Recibida, se guarda
        // el detalle y se suma al stock lo recibido en buen estado. Devuelve false si la orden ya no estaba Pagada.
        public bool Registrar(Recepcion r)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlTransaction tx = con.BeginTransaction();
                try
                {
                    SqlCommand cmdOrden = new SqlCommand(
                        "UPDATE OrdenCompra SET Estado = @Recibida WHERE Id = @Id AND Estado = @Pagada", con, tx);
                    cmdOrden.Parameters.AddWithValue("@Recibida", OrdenCompra.EstadoRecibida);
                    cmdOrden.Parameters.AddWithValue("@Pagada", OrdenCompra.EstadoPagada);
                    cmdOrden.Parameters.AddWithValue("@Id", r.IdOrdenCompra);
                    if (cmdOrden.ExecuteNonQuery() == 0)
                    {
                        tx.Rollback();
                        return false;
                    }

                    SqlCommand cmd = new SqlCommand(@"INSERT INTO Recepcion (IdOrdenCompra, Fecha, Responsable, Comentario)
                        VALUES (@IdOrden, @Fecha, @Responsable, @Comentario); SELECT CAST(SCOPE_IDENTITY() AS INT);", con, tx);
                    cmd.Parameters.AddWithValue("@IdOrden", r.IdOrdenCompra);
                    cmd.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = r.Fecha;
                    cmd.Parameters.AddWithValue("@Responsable", r.Responsable);
                    cmd.Parameters.AddWithValue("@Comentario", (object)r.Comentario ?? DBNull.Value);
                    r.Id = Convert.ToInt32(cmd.ExecuteScalar());

                    foreach (ItemRecepcion item in r.Items)
                    {
                        SqlCommand cmdItem = new SqlCommand(@"INSERT INTO ItemRecepcion
                            (IdRecepcion, IdProducto, CantidadRecibida, Estado, Ubicacion)
                            VALUES (@IdRecepcion, @IdProducto, @Cantidad, @Estado, @Ubicacion)", con, tx);
                        cmdItem.Parameters.AddWithValue("@IdRecepcion", r.Id);
                        cmdItem.Parameters.AddWithValue("@IdProducto", item.Producto.Id);
                        cmdItem.Parameters.AddWithValue("@Cantidad", item.CantidadRecibida);
                        cmdItem.Parameters.AddWithValue("@Estado", item.Estado);
                        cmdItem.Parameters.AddWithValue("@Ubicacion", item.Ubicacion ?? "");
                        cmdItem.ExecuteNonQuery();

                        if (item.Estado == ItemRecepcion.EstadoBueno && item.CantidadRecibida > 0)
                        {
                            SqlCommand cmdStock = new SqlCommand(
                                "UPDATE Producto SET Existencia = Existencia + @Cantidad WHERE Id = @IdProducto", con, tx);
                            cmdStock.Parameters.AddWithValue("@Cantidad", item.CantidadRecibida);
                            cmdStock.Parameters.AddWithValue("@IdProducto", item.Producto.Id);
                            cmdStock.ExecuteNonQuery();
                        }
                    }

                    tx.Commit();
                    return true;
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }
    }
}
