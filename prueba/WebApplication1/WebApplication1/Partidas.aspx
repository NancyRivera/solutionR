<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Partidas.aspx.cs" Inherits="WebApplication1.Partidas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    
 
<div class="container">    
       <h3> Mantenimiento de Partidas</h3>
    
   
        <div class="container-fluid">
       
             <label style="width: 100px" id="Label1">Partida</label>

             <select class="btn btn-secondary btn-sm dropdown-toggle" style="width: 400px" id="CBO_Partida">
             <option value="0">Seleccionar</option>  </select> 

                <div class="form-group" style="width: 400px">
                   <label>Descripcion</label>       
                    <input type ="text"  class="form-control"  id="txtvcDescripcion" />

                </div>
        
            <div>
                <input class="btn btn-primary" type="button" id="btnEditar" value="Editar" />
                <input class="btn btn-success" type="button" id="btnGuardar" value="Guardar" />
                <input class="btn btn-danger" type="button" id="btnEliminarp" value="Eliminar" />
            </div>
        </div>
    
    
        <h3>Mantenimiento Sub Partidas</h3>

        <div class="container-fluid">
            <div class="container-fluid">
            <label style="width: 100px" id="Label2">SubPartida</label>

            <select class="btn btn-secondary btn-sm dropdown-toggle" style="width: 400px" id="CBO_Partidasp">
            <option value="0">Seleccionar</option>   </select>

        </div>

           <div class="form-group" style="width: 400px">
        
              <label>Descripcion</label>        
              <input type="text" class="form-control"  id="txtvcSubpartida" />

            </div>
            <div>   
                <input class="btn btn-primary" type="button" id="btnEditarsp" value="Editar" />
                <input class="btn btn-success" type="button" id="btnGuardarsp" value="Guardar" />
                <input class="btn btn-danger" type="button" id="btnEliminarsp" value="Eliminar" />
            </div>
        </div>


        <h3>Mantenimiento Partida centro costo</h3>
        
        <div class="container-fluid">

            <div>
                <label style="width: 300px" >Partida</label>
                <select class="btn btn-secondary btn-sm dropdown-toggle" style="width: 400px" id="cboPartida">
                <option value="0">Seleccionar</option>  </select>
            </div>
            <div>
                <label style="width: 300px" >Centro de costos</label>
                <select class="btn btn-secondary btn-sm dropdown-toggle" style="width: 400px" id="cboCentroCosto">
                <option value="0">Seleccionar</option> </select>
            </div>
            <div><input class="form-control" style="width:100px" id="txtCodigocosto" type ="text"/></div>
        </div>

        <div class="container-fluid">
            <label>Codigo</label>
            <input id="txtCodigoCC" type ="text"/>
        

        </div>
        <div>  
            <input class="btn btn-success" type="button" id ="btnGuardarCC" value="Guardar" />
        </div>
</div>

    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
    <script src="script/Partida/Partida.js"></script>
</asp:Content>
