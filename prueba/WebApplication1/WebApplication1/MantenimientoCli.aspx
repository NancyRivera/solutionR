<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MantenimientoCli.aspx.cs" Inherits="WebApplication1.MantenimientoCli" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
        
    <div class="row">
        <div class="col-sm-12">
            <div class="form-horizontal">
                <div class="panel panel-primary">
                    <div class="panel-body" id="formBusqueda">
                        <div class="form-group">
                            <div class="col-sm-2">
                                <label>Estado</label>
                                <select id="cboEstado">
                                    <option value="-1">Todos</option>
                                    <option value="0">Inactivo</option>
                                    <option value="1">Activo</option>
                                </select>
                            </div>
                            <div class="col-sm-2">
                                <label>Filtro</label>
                                <input type="text" class="form-control form-control-sm"
                                    id="txtFiltro" />
                            </div>
                        </div>
                    </div>
                    <div class="panel-footer">
                        <div class="form-group">
                            <div class="col-sm-2">
                                <button type="button"
                                    id="btnBuscar"
                                    class="btn btn-primary btn-sm btn-block"
                                    title="Realizar busqueda">
                                    <i class="glyphicon glyphicon-search"></i>&nbsp;
                                </button>
                            </div>
                            <div class="col-sm-2">
                                <button type="button"
                                    id="btnNuevo"
                                    class="btn btn-primary "
                                    title="Agregar Acceso"
                                    data-toggle="modal">
                                    <i class="glyphicon glyphicon-plus"></i>
                                </button>
                            </div>
                            <div class="modal fade" id="ModalRegistrar" tabindex="-1" aria-hidden="true">
                                <div class="modal-dialog" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title">Cliente</h5>
                                        </div>
                                        <div class="modal-body">
                                            <div class="form-group">
                                                <label>Nombre Comercial</label>
                                                <input type="text" class="form-control form-control-sm"
                                                    id="txtNombreComercial" />
                                            </div>
                                            <div class="form-group">
                                                <label>Tipo de Cliente</label>
                                                <select class="form-control form-control-sm" id="cboTipoCliente">
                                                </select>
                                            </div>
                                            <div class="form-group">
                                                <label>Razon social</label>
                                                <input type="text" class="form-control form-control-sm"
                                                    id="txtRazonSocial" />
                                            </div>
                                            <div class="form-group">
                                                <label>Tipo de Documento</label>
                                                <select class="form-control form-control-sm" id="cboTipoDocumento">
                                                </select>
                                            </div>
                                            <div class="form-group">
                                                <label>Razon social</label>
                                                <input type="text" class="form-control form-control-sm"
                                                    id="txtDocIdentidad" />
                                            </div>
                                            <div class="form-group">
                                                <label>Direccion</label>
                                                <input type="text" class="form-control form-control-sm"
                                                    id="txtDireccionFactura" />
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                                            <button type="button" id="btnGuardar" class="btn btn-primary">Guardar</button>
                                            <button type="button" id="btnActualizar" class="btn btn-primary">Actualizar</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <input id="IdCliente" type="hidden" />
                            <div class="col-sm-12 jqGrid" id="dvLisClientes">
                                <table id="dgvLisClientes">
                                </table>
                                <div id="footListado">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
   
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
    <script src='<%= SGOUtil.FileVersion.JavaScript(Context,this.ResolveClientUrl("~/script/Partida/MantenimientoCliente.js")) %>'></script>
</asp:Content>

