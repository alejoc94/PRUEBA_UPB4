using System;

namespace PRUEBA_UPB.DATOS.Constantes
{     internal class ProcAlmacenado
    {
        internal const string spConsultarUsuariosPorId = "SELECT  * FROM sp_consultar_usuarios(@p_id)";
        internal const string spBorrarUsuario = "SELECT  * FROM SP_ELIMINAR_USUARIOS(@p_id)";
        internal const string spGuardarUsuario = "SELECT  * FROM sp_crear_actualizar_usuarios(@p_id, @p_nombre, @p_apellidos, @p_email, @p_username, @p_pdw)";
    }
}