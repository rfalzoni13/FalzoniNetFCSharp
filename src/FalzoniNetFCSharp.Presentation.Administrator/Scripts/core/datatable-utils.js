falzoni.core.dataTableConfiguration = falzoni.core.dataTableConfiguration || {
    loadTable: function (element, isServerSide, url, columnDefs, columns) {
        $.fn.dataTable.ext.errMode = 'none';

        element.on('error.dt', function (e, settings, techNote, message) {
            falzoni.core.configurations.alerts.configAlert("Não foi possível carregar os dados! Tente novamente!", "alert-danger");
            console.error("Ocorreu um erro ao carregar os dados: ", message);
        }).DataTable({
            processing: true,
            serverSide: isServerSide,
            autoWidth: false,
            order: [[0, 'asc']],
            createdRow: function (row, data, dataIndex) {
                $(row).attr('data-id', data.Id);
            },
            fnDrawCallback: function (settings) {
                $('[data-toggle="tooltip"]').tooltip();
            },
            columnDefs: columnDefs,
            ajax: {
                url: url,
                type: 'GET'
            },
            columns: columns
        });
    },

    confirmDeleteData: function (id, url, element, message) {
        falzoni.core.configurations.modals.configModalWarning(message, function () {
            falzoni.core.dataTableConfiguration.deleteData(id, url, element);
        });
    },

    deleteData: function (id, url, element) {

        $.ajax({
            url: url,
            data: { id: id },
            dataType: 'json',
            type: 'POST',
            beforeSend: function () {
                falzoni.core.configurations.showLoading();
            },
            success: function (data) {
                falzoni.core.configurations.hideLoading();
                if (data.success) {
                    falzoni.core.configurations.alerts.configAlert(data.message, "alert-success");
                    $(element).remove();
                    //falzoni.core.configurations.modals.configModalSuccess(data.message, urlCallBack);
                } else {
                    //console.log(data.errors);
                    console.log(data.error);

                    //falzoni.core.configurations.modals.configModalListError(data.errors);
                    falzoni.core.configurations.alerts.configAlert(data.error, "alert-danger");
                    return;
                }
            },
            error: function (xHR, status, error) {
                falzoni.core.configurations.hideLoading();
                console.error(xHR);
                //falzoni.core.configurations.modals.configurarModalErro('Erro ao carregar tabela', error);
                falzoni.core.configurations.alerts.configAlert(xHR.responseJSON.error, "alert-danger");
                return;
            }
        });
    }
}

// Datatable Translate PT-BT
$.extend(true, $.fn.dataTable.defaults, {
    language: {
        "sEmptyTable": "Nenhum registro encontrado",
        "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
        "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
        "sInfoFiltered": "(Filtrados de _MAX_ registros)",
        "sInfoPostFix": "",
        "sInfoThousands": ".",
        "sLengthMenu": "_MENU_ resultados por página",
        "sLoadingRecords": "Carregando...",
        "sProcessing": "Processando...",
        "sZeroRecords": "Nenhum registro encontrado",
        "sSearch": "Pesquisar",
        "oPaginate": {
            "sNext": "Próximo",
            "sPrevious": "Anterior",
            "sFirst": "Primeiro",
            "sLast": "Último"
        },
        "oAria": {
            "sSortAscending": ": Ordenar colunas de forma ascendente",
            "sSortDescending": ": Ordenar colunas de forma descendente"
        },
        "select": {
            "rows": {
                "_": "Selecionado %d linhas",
                "0": "Nenhuma linha selecionada",
                "1": "Selecionado 1 linha"
            }
        },
        "buttons": {
            "copy": "Copiar para a área de transferência",
            "copyTitle": "Cópia bem sucedida",
            "copySuccess": {
                "1": "Uma linha copiada com sucesso",
                "_": "%d linhas copiadas com sucesso"
            }
        }
    }
});