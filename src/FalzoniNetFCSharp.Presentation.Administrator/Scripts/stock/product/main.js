falzoni.product.index = falzoni.product.index || {
    registerComponents: function () {
        moment.locale("pt-br");

        //Definição de colunas
        let columns = [
            { "data": "Name" },
            { "data": "Code" },
            { "data": "Price" },
            { "data": "Category.Name" },            
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
                        data = '<a href="' + falzoni.core.routes.register.product.edit + '?id=' + data.Id + '" class="btn btn-primary" data-toggle="tooltip" data-placement="top" title="Editar"><i class="fa fa-pencil-square-o"></a>';
                    }
                    return data;
                }
            },
            {
                "data": null, "render": function () {
                    return '<button type="button" class="btn btn-danger delete-product" data-toggle="tooltip" data-placement="top" title="Deletar"><i class="fa fa-trash-o"></button>';
                }
            }

        ];

        //Definição de coluna - Remover ordenação dos botões de exclusão e edição
        let columnDefs = [
            { orderable: false, targets: [6, 7] },
        ];

        falzoni.core.dataTableConfiguration.loadTable($("#ProductTable"), false, falzoni.core.routes.register.product.loadTable, columnDefs, columns);

        falzoni.core.configurations.autoLoad();
    },

    deleteProduct: function (id, element) {
        let message = "Deseja remover o produto?"
        falzoni.core.dataTableConfiguration.confirmDeleteData(id, falzoni.core.routes.register.product.delete, element, message);
    }
}

$(document).on("click", ".delete-product", function () {
    var element = $(this).parent().parent();
    var id = $(element).attr("data-id");

    falzoni.product.index.deleteProduct(id, element);
});

$(document).ready(falzoni.product.index.registerComponents());