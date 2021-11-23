
$.jgrid.defaults.responsive = true;

App.FormCliente = {
    credit() {
        this.name = 'Mantenimiento Cliente';
        this.author = 'Rivera';
        this.createAt = 'Hoy 12/11/21';
        return {
            name: this.name,
            createBy: this.author,
            createAt: this.createAt,
            path: this.name + '.aspx/',
            description: 'Contiene metodos y funciones para el formulario MantenimientoCliente' + this.name + '.aspx',
            version: '1.0'
        };
    },
    dataList: [{ IdCliente: 0, DocIdentidad:'', RazonSocial: ' ', Estado:'' }],
    dgvLisClientes: function () {
        jQuery("#dgvLisClientes").jqGrid({
            datatype: 'local',
            data: self.dataList,
            colModel: [
                { label: 'IdCliente', name: 'IdCliente', index: 'IdCliente', key: true, hidden: true, editrules: { edithidden: true } },
                { label: 'Editar', name: 'Edit', index: 'Edit', align: 'center', width: 120, fixed: true, sortable: false, frozen: true,
                    cellattr: function (rowId, value, rowObject, colModel, arrData) {
                        return `class='editar' title='Editar registro' onclick="self.Methods.EditarCliente(${rowObject.IdCliente});"`;
                    }
                },
                { label: 'Documento', name: 'DocIdentidad', index: 'DocIdentidad', width: 150 },
                { label: 'Razon Social', name: 'RazonSocial', index: 'RazonSocial', width: 150 },
                { label: 'Nombre Comercial', name: 'NombreComercial', index: 'NombreComercial', width: 150 },
                { label: 'Estado', name: 'Estado', index: 'Estado', width: 80, align: "right" },
            ],
            rowNum: 1000,
            sortname: 'IdCliente',
            viewrecords: true,
            sortorder: "desc"
            , rownumbers: true,
            height: $(window).width() > 1400 ? $(window).height() - $(window).height() * 0.4 : $(window).height() - $(window).height() * 0.6,
            width: $(window).width() > 1400 ? $(window).width() - $(window).width() * 0.12 : $(window).width() - $(window).width() * 0.2,

        });
    },
    cboEstado() {
        $('#cboEstado').change(function () {
            Estado = $("#cboEstado option:selected").text();

            self.dgvLisClientes();
        });

    },
    gEditar() {
        alert('Editar');
    }

};

App.FormCliente.Methods = {
    ListarClientes(filtro, estado) {
        console.log(filtro);
        console.log(estado);
        $.ajax({
            async: true,
            type: 'POST',
            url: 'MantenimientoCli.aspx/Usp_LisClientes',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ filtro, estado }),
            
            success: function (result) {
                if (result.d) {
                    debugger;
                    var data = result.d;
                    self.dataList = data;

                    $.jgrid.gridUnload('#dgvLisClientes');
                    self.dgvLisClientes();

                }
            },

            error: function (xmlHttpError, error, description) {
                debugger;
                console.log(xmlHttpError.responseText);
                alert(error);
            }

        });
    },

    ValidarCliente: function () {
        let result = false;

        var nombreComercial = $('#txtNombreComercial').val(),
            idTc = document.querySelector('#cboTipoCliente').value,
            razonSocial = $('#txtRazonSocial').val(),
            idTd = document.querySelector('#cboTipoDocumento').value,
            docId = $('#txtDocIdentidad').val(),
            direc = $('#txtDireccionFactura').val();

        if (nombreComercial) {
            result = true;
        } else if (idTc) {
            result = true;
        } else if (razonSocial) {
            result = true;
        } else if (idTd) {
            result = true;
        } else if (docId) {
            result = true;
        } else if (direc) {
            result = true;
        }
        return result;
    },

    RegistrarCliente: function () {
        if (self.Methods.ValidarCliente) {
            var idcl = $('#idcliente').val(),
                nombreComercial = $('#txtNombreComercial').val(),
                idTc = document.querySelector('#cboTipoCliente').value,
                razonSocial = $('#txtRazonSocial').val(),
                idTd = document.querySelector('#cboTipoDocumento').value,
                docId = $('#txtDocIdentidad').val(),
                direc = $('#txtDireccionFactura').val();

            $.ajax({
                async: true,
                type: 'POST',
                url: 'MantenimientoCli.aspx/UspInsCliente',
                data: JSON.stringify({
                    IdCliente: idcl,
                    NombreComercial: nombreComercial,
                    IdTipoCliente: idTc,
                    RazonSocial: razonSocial,
                    IdTipoDocumento: idTd,
                    DocIdentidad: docId,
                    DireccionFactura: direc
                }),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',

                success: function (resp) {

                    if (resp.d) {

                        alert('Registro guardado!');
                        debugger;
                        $('#ModalRegistrar').modal('hide');

                    } else {
                        alert('ya existe');
                    }

                },
                error: function (xmlHttpError, error, description) {
                    debugger;
                    alert(error);
                    console.log(description);

                }

            });
        }

    },

    ListarTipoC() {
        $.ajax({
            async: false,
            type: 'POST',
            url: 'MantenimientoCli.aspx/UspLisTipoCliente',

            contentType: 'application/json; charset=utf-8',

            success: function (result) {
                if (result.d) {
                    var data = result.d;
                    let html = "";
                    $(data).each(function (i, v) {
                        html += `<option value='` + v.IdTipoCliente + `'>` + v.Descripcion + '</option>';

                    });
                    $('#cboTipoCliente').html(html);
                }
            },

            error: function (xmlHttpError, error, description) {
                debugger;
                console.log(xmlHttpError.responseText);
                alert(error);
            }

        });
    },

    ListarDocumento() {
        $.ajax({
            async: false,
            type: 'POST',
            url: 'MantenimientoCli.aspx/UspLisDocumentos',

            contentType: 'application/json; charset=utf-8',

            success: function (result) {
                if (result.d) {
                    var data = result.d;
                    let html = "";
                    $(data).each(function (i, v) {
                        html += `<option value='` + v.IdTipoDocumento + `'>` + v.Descripcion + '</option>';

                    });
                    $('#cboTipoDocumento').html(html);
                }
            },

            error: function (xmlHttpError, error, description) {
                debugger;
                console.log(xmlHttpError.responseText);
                alert(error);
            }

        });
    }


};

App.FormCliente.Events = {
    
    
    btnBuscar() {
        $('#btnBuscar').click(function (){
            
            let estado = $('#cboEstado').val();
            let filtro = $('#txtFiltro').val();
            self.Methods.ListarClientes(filtro, estado);
        })
    },

    btnNuevo() {
        $('#btnNuevo').click(function (){
            $('#ModalRegistrar').modal();
            self.Methods.RegistrarCliente();
            self.Methods.ListarTipoC();
        });
    },
    btnGuardar() {
        $('#btnGuardar').click(function () {

            if (self.Methods.ValidarCliente()) {

                var conf = confirm('Está seguro de realizar esta operación?');

                if (conf) {
                    self.Methods.RegistrarCliente();
                }
            }

             
        });
    },
    btnActualizar() {
        $('#btnActualizar').click(function () {
            var conf = confirm('Actualizar Cliente ?');

            if (conf) {
                self.Methods.gEditar();
            }
        })
    }
};

let self = App.FormCliente;

$(document).ready(function () {
    self.dgvLisClientes();

    self.Events.btnBuscar();
    self.Events.btnNuevo();
    self.Events.btnActualizar();

});