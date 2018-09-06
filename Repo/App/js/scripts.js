$(function () {


    $('.loader').hide();

    $('#mostrar').on("click", function (){
    debugger;
    if ($('#tbfechaini').val() == "") {
            var status = "<a class=\"close\" data-dismiss=\"alert\">×</a><strong>¡Error!</strong>"
                + " Favor de llenar todos los campos.";
            $('#divstatus').html("<div>" + status + "</div>");
            return;
        }
     var xmlhttp = new XMLHttpRequest();
     if(xmlhttp != null){
          var url = "clases/princ.aspx";
            xmlhttp.onreadystatechange = function () {
                if (xmlhttp.readyState == 4) {
                    var resultado = xmlhttp.responseText;

                    if (resultado != "") {
                        $('#divtabla').html(resultado);
                    }
                }
            };
            xmlhttp.open("POST", url, true);
            xmlhttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            xmlhttp.send("fini=" + $('#tbfechaini').val()+"&ffin="+$('#tbfechafin').val()+"&cen="+$('#sel1').val());
     }
        
    });


    $('#buscar').on("click", function JqueryAjaxCall(){
    debugger;
    $('.loader').show();
    if ($('#tbfechaini').val() == "") {
            var status = "<a class=\"close\" data-dismiss=\"alert\">×</a><strong>¡Error!</strong>"
                + " Favor de llenar todos los campos.";
            $('#divstatus').html("<div>" + status + "</div>");
            return;
        }
     var pageUrl = "clases/princ.aspx/JqueryAjaxCall"; 
     var fini = $('#tbfechaini').val();
     var ffin = $('#tbfechafin').val();
     var cen = $('#sel1').val();
     var linea = $('#linea').val();
     var parameter = { "feini": fini, "fefin": ffin, "centro": cen, "linea": linea }

     $.ajax({
        type: 'POST',
        url: pageUrl,
        data: JSON.stringify(parameter),
        contentType: 'application/json; charset= utf-8',
        dataType: 'json',
        success: function(data){
            mostrartbprinc(data);
        },
        error: function(data, success, error){
            alert("Error : "+ error);
        }

        });
      return false;  
     });

     function mostrartbprinc(data){        
        $('#tablaini').css("display", "none");
        $('.loader').hide();
        $('#atrasprinc').css("display", "block"); 
        $('#divtabla').css("display", "block"); 
        $('#divtabla').html(data.d);
     }


      function mostrartbint(data){
        $('#divtabla').css("display", "none"); 
        $('#atrasprinc').css("display", "none");
        $('.loader').hide();
        $('#atrasmed').css("display", "block");
        $('#divtablaint').css("display", "block");
        $('#divtablaint').html(data.d);
    
     }

        function mostrartbtipo(data){
        $('#divtablaint').css("display", "none"); 
        $('#atrasmed').css("display", "none"); 
        $('.loader').hide();
        $('#atrasultimo').css("display", "block");
        $('#divtablatipo').css("display", "block");
        $('#divtablatipo').html(data.d);
    
     }

     


     $('#divtabla').delegate("tr", "click", function() {
         debugger;
       //var i = $('tr').index(this);

        //$('#divtabla table tbody tr').eq(i-1).after("<div>Test</div>");

        $('.loader').show();

        var pageUrlof = "clases/princ.aspx/FilOrdFab";
        var trs = $(this);
        var ofa = trs[0].cells[0].innerHTML;
        var parameter = { "ofa": ofa}

        $.ajax({
            type: 'POST',
            url: pageUrlof,
            data: JSON.stringify(parameter),
            contentType: 'application/json; charset= utf-8',
            dataType: 'json',
            success: function(data){
               mostrartbint(data)
               
            },
            error: function(data, success, error){
                alert("Error : "+ error);
            }

        });
        
             

    });


     $('#divtablaint').delegate("tr", "click", function() {
        debugger;

        $('.loader').show();
        var pageUrlof = "clases/princ.aspx/Filtipo";
        var trs = $(this);
        var ofa = trs[0].cells[0].innerHTML;
        var tipo = trs[0].cells[1].innerHTML; 
        var costost = trs[0].cells[7].innerHTML; 

        var parameter = { "ofa": ofa, "tipo": tipo, "costost": costost }

        $.ajax({
            type: 'POST',
            url: pageUrlof,
            data: JSON.stringify(parameter),
            contentType: 'application/json; charset= utf-8',
            dataType: 'json',
            success: function(data){
               mostrartbtipo(data)
               
            },
            error: function(data, success, error){
                alert("Error : "+ error);
            }

        });
        
    }); 
    
});

    function reginicio(){
        debugger;
        $('#atrasprinc').css("display", "none"); 
        $('#divtabla').css("display", "none"); 
        $('#tablaini').css("display", "block");
     }

     function regmed(){
        debugger;
        $('#atrasmed').css("display", "none"); 
        $('#atrasprinc').css("display", "block"); 
        $('#divtabla').css("display", "block"); 
        $('#divtablaint').css("display", "none");
     }

      function regultimo(){
        debugger;
        $('#atrasultimo').css("display", "none"); 
        $('#atrasmed').css("display", "block"); 
        $('#divtablaint').css("display", "block"); 
        $('#divtablatipo').css("display", "none");
     }



