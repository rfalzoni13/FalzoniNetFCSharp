falzoni.user.index = falzoni.user.index || {
    registerComponents: function () {
        moment.locale("pt-br");

        //Definição de colunas
        let columns = [
            { "data": "Name" },
            { "data": "Email" },
            { "data": "UserName" },
            { "data": "Gender" },
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
                        data = '<a href="' + falzoni.core.routes.register.user.edit + '?id=' + data.Id + '" class="btn btn-primary" data-toggle="tooltip" data-placement="top" title="Editar"><i class="fa fa-pencil-square-o"></a>';
                    }
                    return data;
                }
            },
            {
                "data": null, "render": function () {
                    return '<button type="button" class="btn btn-danger delete-user" data-toggle="tooltip" data-placement="top" title="Deletar"><i class="fa fa-trash-o"></button>';
                }
            }

        ];

        //Column Definitions - Remove edit and delete buttons order
        let columnDefs = [
            { orderable: false, targets: [6, 7] },
        ];

        falzoni.core.dataTableConfiguration.loadTable($("#UserTable"), false, falzoni.core.routes.register.user.loadTable, columnDefs, columns);

        falzoni.core.configurations.autoLoad();
    },

    deleteUser: function (id, element) {
        let message = "Deseja remover o usuário?"
        falzoni.core.dataTableConfiguration.confirmDeleteData(id, falzoni.core.routes.register.user.delete, element, message);
    },
}

$(document).on("click", ".delete-user", function () {
    var element = $(this).parent().parent();
    var id = $(element).attr("data-id");

    falzoni.user.index.deleteUser(id, element);
});

$(document).ready(falzoni.user.index.registerComponents());