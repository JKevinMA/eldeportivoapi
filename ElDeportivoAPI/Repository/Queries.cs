using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElDeportivoAPI.Repository
{
    public class Queries
    {
        // Login
        public static string Login = "Select T.*,R.DESCRIPCION AS ROL,A.DESCRIPCION AS AREA from TRABAJADOR T INNER JOIN ROL R ON R.IDROL = T.IDROL INNER JOIN AREA A ON A.IDAREA = T.IDAREA where USUARIO = @USUARIO and CONTRASENA = @CONTRASENA";

        // Generar orden de reposición
        public static string ObtenerCategorias = "Select * from categoria";
        public static string ObtenerMaterialesDeficit = "Select codigomaterial,nombre,m.descripcion,marca,stock,precio,m.idcategoria,c.descripcion as categoria,presentacion,cantidadminima,limite from material m inner join categoria c on c.idcategoria = m.idcategoria where stock <= cantidadminima and m.idcategoria = @idcategoria";
        public static string ObtenerNuevoNroOrden = "ObtenerNuevoNroOrden";
        public static string RegistrarNuevaOrden = "Insert into ORDEN (IDORDEN,CONCEPTO,IDSOLICITANTE,FECHAGENERADA,NROITEMS,ESTADO,IDAREA) values ( @idorden,@concepto,@idsolicitante,GETDATE(),@nroitems,@estado,@idarea)";
        public static string RegistrarDetalleOrden = "Insert into ordendetalle(IDORDEN,CODIGOMATERIAL,CANTIDADREQUERIDA) values ( @idorden,@codigomaterial,@cantidadrequerida)";

        // Generar solicitud de cotizacion para proveedores
        public static string ObtenerOrdenes = "Select * from orden where concepto = @concepto and estado=@estado order by fechagenerada";
        public static string ObtenerDetalleOrden = "Select od.*,m.descripcion as material,c.presentacion from ordendetalle od inner join material m on m.codigomaterial = od.codigomaterial inner join categoria c on c.idcategoria = m.idcategoria where idorden = @idorden";
        public static string ActualizarEstadoOrden = "Update orden set estado=@estado where idorden=@idorden";
        public static string BuscarProveedor = "Select * from proveedor";
        public static string ObtenerNuevoNroSolicitudCotizacion = "ObtenerNuevoNroSolicitudCotizacion";
        public static string RegistrarSolicitudCotizacion = "Insert into solicitudcotizacion(Idsolicitudcotizacion,idtrabajador,modalidadpago,fechagenerada,fechalimite,idorden) values(@Idsolicitudcotizacion,@idtrabajador,@modalidadpago,GETDATE(),@fechalimite,@idorden)";
        public static string RegistrarDetalleSolicitudCotizacion = "Insert into proveedorcotizacion(Idsolicitudcotizacion,ruc) values(@Idsolicitudcotizacion,@ruc)";

        // Generar orden de compra
        public static string ObtenerNuevoNroOrdenCompra = "ObtenerNuevoNroOrdenCompra";
        public static string ObtenerSolicitudCotizacion = "select * from solicitudcotizacion where idsolicitudcotizacion = @idsolicitudcotizacion";
        public static string ObtenerProveedoresCotizacion = "Select p.* from proveedorcotizacion pc inner join proveedor p on p.ruc = pc.ruc where pc.idsolicitudcotizacion = @idsolicitudcotizacion ";
        public static string RegistrarNuevaOrdenCompra = "Insert into ORDENCOMPRA (IDORDENCOMPRA,IDSOLICITUDCOTIZACION,RUC,COSTOENVIO,SUBTOTAL,IMPUESTO,FECHAGENERADA,IDTRABAJADOR,RUTAPROFORMA,ESTADO) values ( @IDORDENCOMPRA, @IDSOLICITUDCOTIZACION,   @RUC, @COSTOENVIO,  @SUBTOTAL, @IMPUESTO,    GETDATE(), @IDTRABAJADOR,    @RUTAPROFORMA,@ESTADO)";                                  
        public static string RegistrarDetalleOrdenCompra = "Insert into ordencompradetalle(IDORDENCOMPRA,CODIGOMATERIAL,CANTIDAD,PRECIOUNITARIO) values (@IDORDENCOMPRA,@CODIGOMATERIAL,@CANTIDAD,@PRECIOUNITARIO)";

        // Obtener Pedidos
        public static string ObtenerOrdenesPedidos = "select op.*,cl.NOMBRES ,cl.APELLIDOS ,cl.DIRECCION+' - '+d.descripcion as DIRECCION,cl.TELEFONO,d.descripcion as DISTRITO from ORDENPEDIDO op inner join COTIZACION c on c.IDCOTIZACION = op.IDCOTIZACION inner join CLIENTE cl on cl.NRODOCUMENTO = c.NRODOCUMENTO inner join distrito d on d.iddistrito = cl.iddistrito WHERE CONVERT(NVARCHAR(10),FECHAENTREGA,23) = @fecha AND ESTADO = @estado";
        public static string ObtenerCotizacionDetalle = "SELECT CD.*,P.DESCRIPCION +' '+ M.DESCRIPCION AS PRENDA,T.DESCRIPCION AS TALLA FROM COTIZACIONDETALLE CD INNER JOIN PRENDA P ON P.IDPRENDA = CD.IDPRENDA INNER JOIN MATERIAL M ON M.CODIGOMATERIAL = CD.CODIGOMATERIAL INNER JOIN TALLA T ON T.IDTALLA = CD.IDTALLA WHERE CD.IDCOTIZACION = @IDCOTIZACION ";

        // Generar guia de remision
        public static string ObtenerTransportistas = "select t.*,d.descripcion as distrito from transportista t inner join distrito d on t.iddistrito = d.iddistrito where t.iddistrito = @iddistrito";
        public static string ObtenerDistritos = "SELECT * FROM DISTRITO";
        public static string ObtenerNuevoNroGuiaRemision = "ObtenerNuevoNroGuiaRemision";
        public static string RegistrarNuevaGuiaRemision = "Insert into guiaremision(idguiaremision,fechaemision,idordenpedido,idtransportista,vehiculo,placa,modelo) values(@idguiaremision,getdate(),@idordenpedido,@idtransportista,@vehiculo,@placa,@modelo)";
    }
}
