'use strict';

//Make sure jQuery has been loaded before app.js
if (typeof jQuery === "undefined") {
    throw new Error("Siddhi requires jQuery");
}

$.Siddhi = {};

$.Siddhi.language = {
    emptyTable: 'No se encontraron datos',
    info: 'Mostrando <strong>_START_</strong> a <strong>_END_</strong> de <strong>_TOTAL_</strong> registros</h4>',
    infoEmpty: 'Mostrando 0 a 0 de 0 registros',
    infoFiltered: '<span class="label label-info"><i class="fa fa-filter"></i> filtrado de _MAX_ registros</span>',
    lengthMenu: 'Mostrar _MENU_ registros',
    loadingRecords: 'Cargando...',
    processing: 'Procesando...',
    zeroRecords: 'No hay registros',
    search: '',
    paginate: {
        firs: 'Primero',
        last: 'Último',
        next: 'Siguiente',
        previous: 'Anterior'
    }
};


function _init() {
    $.extend($.fn.dataTable.defaults, {
        language: $.Siddhi.language,
        processing: true,
        lengthMenu: [[10, 25, 50, 100], [10, 25, 50, 100]],
        displayLength: 10
    });
};

function _styles() {
    // adding custom styles to all .datatables
    $('.datatables').each(function () {
        Console.log('INICIA');
        var datatables = $(this);
        // SEARCH - Add the placeholder for Search and Turn this into in-line form control
        var searchInput = datatables.closest('.dataTables_wrapper').find('div[id$=_filter] input');
        Console.log(searchInput);
        searchInput.attr('placeholder', 'Buscar');
        searchInput.addClass('form-control input-sm pull-right');
        // LENGTH - Inline-Form control
        var lengthSel = datatables.closest('.dataTables_wrapper').find('div[id$=_length] select');
        lengthSel.addClass('form-control input-sm');
        // lengthSel.wrap( '<label class="select select-sm"></label>' );
        // Paginations
        var paginations = datatables.closest('.dataTables_wrapper').find('.dataTables_paginate').children('.pagination');

        paginations.addClass('pagination-sm pagination-rounded');
    }).on('draw.dt', function () {
        // update sidebar height
        $('.sidebar').wrapkitSidebar('updateHeight');
    });
};

/***
 * Esta función solo se debe usar para tablas planas
 * no necesita JSON
 * @param table
 * @returns
 */
function _simpleTable(table) {
    var dataTable = $(table).DataTable({
        language: $.Siddhi.language,
        sorting: false,
        ordering: false,
        processing: true,
        autoWidth: false
    });

    _styles();

    return dataTable;
};


/***
 * Función para datos con JSON en ventanas modales
 * @param table
 * @param dest
 * @param cols
 * @returns
 */
function _jsonModalTable(table, dest, cols) {
    var dataTable = $(table).DataTable({
        language: $.Siddhi.language,
        processing: true,
        serverSide: true,
        sorting: false,
        ordering: false,
        ajax: {
            url: dest,
            type: "POST"
        },
        columns: cols,
        fnInitComplete: function (oSettings, json) {
            //custom search box
            $(table + '_filter').empty().append('<div class="input-group input-group-sm input-group-in"><input type="search"  class="form-control" placeholder="Buscar"><div class="input-group-btn"><button id="filter" type="button" class="btn btn-primary"><i class="fa fa-search"></i></button></div></div>');

            //Unbind search box
            $(table + '_filter input').unbind().bind('keyup', function (e) {
                if (e.keyCode == 13) {
                    $(table + '_filter button').click();
                }
            });

            //Click search
            $(table + '_filter button').click(function (e) {
                dataTable.search($(table + '_filter input').val()).draw();
            });
        }
    });

    _styles();

    return dataTable;
};

/***
 * Función para tablas con pocos camopos 
 * @param table
 * @param dest
 * @param cols
 * @returns
 */
function _jsonSimpleTable(table, dest, cols) {
    var dataTable = $(table).DataTable({
        language: $.Siddhi.language,
        processing: true,
        serverSide: true,
        sorting: false,
        ordering: false,
        ajax: {
            url: dest,
            type: "POST"
        },
        columns: cols,
        fnInitComplete: function (oSettings, json) {
            //custom search box
            $(table + '_filter').empty().append('<div class="input-group input-group-sm input-group-in"><input type="search" id="search" class="form-control" placeholder="Buscar"><div class="input-group-btn"><button id="filter" type="button" class="btn btn-primary"><i class="fa fa-search"></i></button></div></div>');

            $(table + '_filter').empty().append('<div class="input-group input-group-sm input-group-in"><input type="search"  class="form-control" placeholder="Buscar"><div class="input-group-btn"><button id="filter" type="button" class="btn btn-primary"><i class="fa fa-search"></i></button></div></div>');

            //Unbind search box
            $(table + '_filter input').unbind().bind('keyup', function (e) {
                if (e.keyCode == 13) {
                    $(table + '_filter button').click();
                }
            });

            //Click search
            $(table + '_filter button').click(function (e) {
                dataTable.search($(table + '_filter input').val()).draw();
            });
        }
    });

    _styles();

    return dataTable;
};

function _jsonComposedTable(table, dest, cols) {
    var dataTable = $(table).DataTable({
        language: $.Siddhi.language,
        processing: true,
        serverSide: true,
        sorting: false,
        ordering: false,
        searching: false,
        ajax: {
            url: dest,
            type: "POST",
            data: function (d) {
                //d.myKey = "myValue";
                //d.custom = $('#myInput').val();
                // etc
            }
        },
        columns: cols
    });

    _styles();

    return dataTable;
};

function _jsonGroupingTable(table, dest, cols, index, colspan) {
    var dataTable = $(table).DataTable({
        language: $.Siddhi.language,
        processing: true,
        serverSide: true,
        sorting: false,
        ordering: false,
        searching: false,
        columnDefs: [
	                   { visible: false, targets: index }
        ],
        drawCallback: function (settings) {
            var api = this.api();
            var rows = api.rows({ page: 'current' }).nodes();
            var last = null;

            api.column(index, { page: 'current' }).data().each(function (group, i) {
                if (last !== group) {
                    $(rows).eq(i).before(
                        '<tr class="group"><td colspan="' + colspan + '"><span class="badge badge-primary">' + group + ' </span></td></tr>'
                    );

                    last = group;
                }
            });
        },
        ajax: {
            url: dest,
            type: "POST",
            data: function (d) {
            }
        },
        columns: cols
    });

    _styles();

    return dataTable;
};

$(function () {
    //Set up the object
    _init();
});