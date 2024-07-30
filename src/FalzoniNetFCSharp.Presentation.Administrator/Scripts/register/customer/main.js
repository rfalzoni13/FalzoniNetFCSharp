falzoni.customer.index = falzoni.customer.index || {
    registerComponents: function () {
        moment.locale("pt-br");

        //Definição de colunas
        let columns = [
            { "data": "Nome" },
            { "data": "Gender" },
            { "data": "Email" },
            { "data": "Document" },
            {
                "data": "Created", "render": function (value) {
                    if (value === null) return "";
                    return moment(value).format('LLL');
                }
            },
            {
                "data": "Modified", "render": function (value) {
                    if (value === null) return "Nunca modificado";
                    return moment(value).format('LLL');
                }
            },
            {
                "data": null, "render": function (data, type) {
                    if (type === 'display') {
                        data = '<a href="' + falzoni.core.routes.register.customer.edit + '?id=' + data.Id + '" class="btn btn-primary" data-toggle="tooltip" data-placement="top" title="Editar"><i class="fa fa-pencil-square-o"></a>';
                    }
                    return data;
                }
            },
            {
                "data": null, "render": function () {
                    return '<button type="button" class="btn btn-danger delete-customer" data-toggle="tooltip" data-placement="top" title="Deletar"><i class="fa fa-trash-o"></button>';
                }
            }

        ];

        //Definição de coluna - Remover ordenação dos botões de exclusão e edição
        let columnDefs = [
            { orderable: false, targets: [6, 7] },
        ];

        falzoni.core.dataTableConfiguration.loadTable($("#CustomerTable"), false, falzoni.core.routes.register.customer.loadTable, columnDefs, columns);

        falzoni.core.configurations.autoLoad();
    },

    deleteCustomer: function (id, element) {
        let message = "Deseja remover o cliente?"
        falzoni.core.dataTableConfiguration.confirmDeleteData(id, falzoni.core.routes.register.customer.delete, element, message);
    }
}

$(document).on("click", ".delete-customer", function () {
    var element = $(this).parent().parent();
    var id = $(element).attr("data-id");

    falzoni.customer.index.deleteCustomer(id, element);
});

$(document).ready(falzoni.customer.index.registerComponents());