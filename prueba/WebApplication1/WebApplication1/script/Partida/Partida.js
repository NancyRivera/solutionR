const App = {};
App.FormPartida = {
    credit() {
        this.name = 'Partidas';
        this.author = 'Rivera';
        this.createAt = 'Hoy 26/10/21'; 
        return {
            name: this.name,
            createBy: this.author,
            createAt: this.createAt,
            path:  this.name + '.aspx/',
            description: 'Contiene metodos y funciones para el formulario ' + this.name + '.aspx',
            version: '1.0'
        };
    }, 
};

App.FormPartida.Events = {
    
    btnGuardarClick()
    {
        $('#btnGuardar').click(function () {
            let descr = $('#txtvcDescripcion').val();
            debugger;
            $.ajax({
                async: true,
                type: 'POST',
                url: 'Partidas.aspx/InsertarPartida',
                data: JSON.stringify({ vcDescripcion: descr }),
                contentType: 'application/json; charset=utf-8',

                success: function (result) {
                    if (result.d) {
                        var data = result.d;
                        
                        self.Methods.ListarPartida();
                        alert('Partida guardada!')
                    }
                },
                error: function (xmlHttpError, error, description) {
                    debugger;
                    console.log(xmlHttpError.responseText);
                    alert(error);
                }
            });
        });
        
    },
    
    btnGuardarSubPartidaClick() {
        $('#btnGuardarsp').click(function () {
            let vcsubpartida = $('#txtvcSubpartida').val();
            let inpar = $('#CBO_Partida').val();
            debugger;
            $.ajax({
                async: true,
                type: 'POST',
                url: 'Partidas.aspx/InsertarSubPartida',
                data: JSON.stringify({
                    vcSubpartida: vcsubpartida,
                    InPartida: inpar
                }),
                contentType: 'application/json; charset=utf-8',

                success: function (result) {
                    if (result.d) {
                        var data = result.d;
                        self.Methods.ListarSubPartida();
                        alert('Subpartida guardada!')
                    }
                },
                error: function (xmlHttpError, error, description) {
                    debugger;
                    console.log(xmlHttpError.responseText);
                    alert(error);
                }
            });
        });

    },

    btnccGuardarClick() {
        $('#btnGuardarCC').click(function () {
            let cod = $('#txtCodigoCC').val();
            let idcc = $('#cboCentroCosto').val();
            let idpar = $('#cboPartida').val();
            
            debugger;
            $.ajax({
                async: true,
                type: 'POST',
                url: 'Partidas.aspx/InsertarCeCo',
                data: JSON.stringify({
                    Codigo: cod,
                    IdGastoCentroCosto: idcc,
                    IdPartida: idpar
                }),
                contentType: 'application/json; charset=utf-8',
                
                success: function (result) {
                    if (result.d) {
                        var data = result.d;
                        alert('Codigo guardado!')
                    }
                },
                error: function (xmlHttpError, error, description) {
                    debugger;
                    console.log(xmlHttpError.responseText);
                    alert(error);
                }
            });
        });

    },

   cbopartidachange() {
        $('#CBO_Partida').change(function () {
            let idpa = $(this).val();
            let vcdes = $("#CBO_Partida option:selected").text();
            $('#txtvcDescripcion').val(vcdes);
            self.Methods.ListarSubPartida();
        });
    },
   
    cbosubpartidachange() {
        $('#CBO_Partidasp').change(function () {
            let idsp = $(this).val();
            let vcsubpartida = $("#CBO_Partidasp option:selected").text();
            $('#txtvcSubpartida').val(vcsubpartida);
        });
    },

    btmostrarcod() {
        $('#btnGuardarCC').click(function () {
            let idcc = $(this).val();
            let vccod = $("#txtCodigoCC option:selected").text();
            $('#txtCodigocosto').val(vccod);
        });
    },


    DesactivarPartida() {
     
        $('#btnEliminarp').click(function () {
            let idpar = $('#CBO_Partida').val();
            //alert(0);

            debugger;
            $.ajax({
                async: true,
                type: 'POST',
                url: 'Partidas.aspx/DesactivarPartida',

                data: JSON.stringify({ IdPartida: idpar, InEstado: 0 }),

                contentType: 'application/json; charset=utf-8',

                success: function (result) {
                    if (result.d) {
                        var data = result.d;
                        self.Methods.ListarPartida();
                        alert('Partida eliminada')
                    }
                },

                error: function (xmlHttpError, error, description) {
                    debugger;
                    console.log(xmlHttpError.responseText);
                    alert(error);
                }

            });

        });
    },

    DesactivarSubPartida() {
        $('#btnEliminarsp').click(function () {
            let inpar = $('#CBO_Partida').val();
            let idsp = $('#CBO_Partidasp').val();
            debugger;
            $.ajax({
                async: true,
                type: 'POST',
                url: 'Partidas.aspx/DesactivarSubPartida',

                data: JSON.stringify({ InPartida: inpar, IdSubpartida: idsp, InEstado: 0 }),

                contentType: 'application/json; charset=utf-8',

                success: function (result) {
                    if (result.d) {
                        var data = result.d;
                        self.Methods.ListarPartida();
                        alert('Subpartida eliminada')
                    }
                },

                error: function (xmlHttpError, error, description) {
                    debugger;
                    console.log(xmlHttpError.responseText);
                    alert(error);
                }

            });

        });
    }
};


//METHODS
var self = App.FormPartida;
App.FormPartida.Methods = {
    ListarPartida() {
    
        $.ajax({
            async: true,
            type: 'POST',
            url: 'Partidas.aspx/verPartidaVW',
            //dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            
            success: function (result) {
                if (result.d) {
                    var data = JSON.parse(result.d);

                    var html = `<option value='-1'>[SELECCIONE]</option>`;
                    $(data).each(function (i, v) { 
                        html += `<option value='` + v.IdPartida + `'>` + v.vcDescripcion + '</option>';
                    });
                    $('#CBO_Partida').html(html);                     
                    $('#cboPartida').html(html);

                }
            },

            error: function (xmlHttpError, error, description) {
                debugger;
                console.log(xmlHttpError.responseText);
                alert(error);
            }
            
        });
    },
    ListarCC() {
    
        $.ajax({
            async: true,
            type: 'POST',
            url: 'Partidas.aspx/MostrarCC',
          
            contentType: 'application/json; charset=utf-8',
            
            success: function (result) {
                if (result.d) {
                    var data = JSON.parse(result.d);

                    var html = `<option value='-1'>[SELECCIONE]</option>`;
                    $(data).each(function (i, v) { 
                        html += `<option value='` + v.IdGastoCentroCosto +
                            `'>` + v.Descripcion + '</option>';
                    });
                    $('#cboCentroCosto').html(html);
                }
            },

            error: function (xmlHttpError, error, description) {
                debugger;
                console.log(xmlHttpError.responseText);
                alert(error);
            }
            
        });
    },

    ListarSubPartida() {
        let Inpartida = $('#CBO_Partida').val();

        $.ajax({
            async: true,
            type: 'POST',
            url: 'Partidas.aspx/verSubPartidaVW',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ Inpartida: Inpartida }),
            success: function (result) {
                if (result.d) {
                    var data = JSON.parse(result.d);
                    var html = `<option value='-1'>[SELECCIONE]</option>`;
                    $(data).each(function (i, v) {
                        html += `<option value='` + v.IdSubpartida + `'>` + v.vcSubpartida + '</option>';
                    });
                    $('#CBO_Partidasp').html(html);
                }
            },

            error: function (xmlHttpError, error, description) {
                debugger;
                console.log(xmlHttpError.responseText);
                alert(error);
            }

        });
    },
     
    
    EditarPartida() {

        $('#btnEditar').click(function () {
            let descr = $('#txtvcDescripcion').val();
            let idpartida = $('#CBO_Partida').val();
            debugger;
            $.ajax({
                async: true,
                type: 'POST',
                url: 'Partidas.aspx/EditarPartida',
                data: JSON.stringify({ IdPartida: idpartida, vcDescripcion: descr }),
                
                contentType: 'application/json; charset=utf-8',

                success: function (result) {
                    if (result.d) {
                        var data = result.d;
                        self.Methods.ListarPartida();

                    }
                },

                error: function (xmlHttpError, error, description) {
                    debugger;
                    console.log(xmlHttpError.responseText);
                    alert(error);
                }

            });

        });
    },
           
    EditarSubPartida() {

        $('#btnEditarsp').click(function () {
            let vcsubpartida = $('#txtvcSubpartida').val();
            let idsp = $('#CBO_Partidasp').val();
            debugger;
            $.ajax({
                async: true,
                type: 'POST',
                url: 'Partidas.aspx/EditarSubPartida',
                data: JSON.stringify({
                    vcSubpartida: vcsubpartida,
                    IdSubpartida: idsp
                }),

                contentType: 'application/json; charset=utf-8',
                
                success: function (result) {
                    if (result.d) {
                        var data = result.d;
                        self.Methods.ListarSubPartida();

                    }
                },
                
                error: function (xmlHttpError, error, description) {
                    debugger;
                    console.log(xmlHttpError.responseText);
                    alert(error);
                }

            });

        });
    },
    

};

//ready
$(document).ready(function () {
    self.Methods.ListarPartida();
    self.Methods.ListarCC();
    self.Methods.ListarSubPartida();

    self.Methods.EditarPartida();
    self.Methods.EditarSubPartida();

    self.Events.DesactivarPartida();
    self.Events.DesactivarSubPartida();
 

    self.Events.cbopartidachange();
    self.Events.cbosubpartidachange();
    self.Events.btmostrarcod();

    self.Events.btnGuardarClick();
    self.Events.btnGuardarSubPartidaClick();
    self.Events.btnccGuardarClick();
});
