falzoni.customer.register = falzoni.customer.register || {
    registerComponents: function () {
        $("#AddAddress").click(falzoni.customer.register.addAddress);
        $("#ShowFormAddress").click(function () {
            falzoni.core.configurations.showLoading();
            setTimeout(function () {
                falzoni.customer.register.showForm();
                falzoni.core.configurations.hideLoading();
            }, 500)
        });

        $("#HideFormAddress").click(function () {
            falzoni.core.configurations.showLoading();
            setTimeout(function () {
                falzoni.customer.register.hideForm();
                falzoni.customer.register.clearAddressForm();
                falzoni.core.configurations.hideLoading();
            }, 500)
        });


        if ($("#CustomerAddressTable tbody:last > tr").length > 0) {
            $(".edit-address").click(falzoni.customer.register.prepareAddressEdit);
            $(".delete-address").click(falzoni.customer.register.removeAddress);
        }

        $("#Postal").blur(falzoni.customer.register.searchCep);

        falzoni.core.configurations.autoLoad();

        falzoni.core.datepicker.registerConfigurations();

        $(".form-customer").submit(falzoni.customer.register.validateForm);
    },

    // Configurações da página
    showForm: function () {
        if ($(".box-form-address").hasClass("d-none"))
            $(".box-form-address").removeClass("d-none");

        $("#ShowFormAddress").addClass("d-none");
    },

    hideForm: function () {
        if (!$(".box-form-address").hasClass("d-none"))
            $(".box-form-address").addClass("d-none")

        $("#ShowFormAddress").removeClass("d-none");
    },

    removeEmptyRegisterMessage: function () {
        if (!$(".no-address").hasClass('d-none')) {
            $(".no-address").addClass('d-none')
        }
    },

    addRegisterMessage: function () {
        if ($(".no-address").hasClass('d-none')) {
            $(".no-address").removeClass('d-none')
        }
    },

    removeTable: function () {
        if (!$(".table-responsive").hasClass('d-none')) {
            $(".table-responsive").addClass('d-none')
        }

        if ($(".title-table-address").hasClass('d-none')) {
            $(".title-table-address").removeClass('d-none')
        }

        if (!$(".title-table-address").hasClass('d-none')) {
            $(".title-table-address").addClass('d-none')
        }
    },

    addTable: function () {
        if ($(".table-responsive").hasClass('d-none')) {
            $(".table-responsive").removeClass('d-none')
        }

        if ($(".title-table-address").hasClass('d-none')) {
            $(".title-table-address").removeClass('d-none')
        }
    },

    clearAddressForm: function () {
        $("#IndexTable").html("");
        $("#Postal").val('');
        $("#Street").val('');
        $("#Num").val('');
        $("#Comp").val('');
        $("#Region").val('');
        $("#District").val('');
        $("#UF").val('');
    },

    addAddress: function () {
        falzoni.core.configurations.showLoading();
        setTimeout(function () {
            let index = 0;

            if ($("#CustomerAddressTable tbody:last > tr").length > 0) {
                index = $("#CustomerAddressTable tbody:last > tr").length;
            }

            let postalCode = $("#Postal").val();
            let addressName = $("#Street").val();
            let number = $("#Num").val();
            let complement = $("#Comp").val();
            let neighborhood = $("#Region").val();
            let city = $("#District").val();
            let state = $("#UF").val();

            if (postalCode == '' || addressName == '' || number == '' || neighborhood == '' || city == '' || state == '') {
                falzoni.core.configurations.hideLoading();
                return;
            }

            $("#CustomerAddressTable > tbody").append(`<tr><td><input id="Addresses_${index}__PostalCode" name="Addresses[${index}].PostalCode" 
            type="hidden" value="${postalCode}">${postalCode}</td><td><input id="Addresses_${index}__AddressName" name="Addresses[${index}].AddressName"
            type="hidden" value="${addressName}">${addressName}</td><td><input data-val="true" id="Addresses_${index}__Number" 
            name="Addresses[${index}].Number" type="hidden" value="${number}">${number}</td><td><input id="Addresses_${index}__Complement" 
            name="Addresses[${index}].Complement" type="hidden" value="${complement}">${complement}</td><td><input id="Addresses_${index}__Neighborhood" 
            name="Addresses[${index}].Neighborhood" type="hidden" value="${neighborhood}">${neighborhood}</td><td><input id="Addresses_${index}__City" 
            name="Addresses[${index}].City" type="hidden" value="${city}">${city}</td><td><input id="Addresses_${index}__State" 
            name="Addresses[${index}].State" type="hidden" value="${state}">${state}</td><td><button type="button" class="btn btn-info edit-address"
            data-toggle="tooltip" data-placement="top" title="Editar"><i class="fa fa-pencil-square-o"></i></button></td>
            <td><button type="button" class="btn btn-danger delete-address" data-toggle="tooltip" data-placement="top" title="Deletar">
            <i class="fa fa-trash-o"></i></button></td></tr>`);

            falzoni.customer.register.removeEmptyRegisterMessage();
            falzoni.customer.register.addTable();

            falzoni.customer.register.clearAddressForm();


            let editElement = $("#CustomerAddressTable tbody:last > tr > td > button.edit-address")
            let removeElement = $("#CustomerAddressTable tbody:last > tr > td > button.delete-address")

            $(editElement).click(falzoni.customer.register.prepareAddressEdit);
            $(removeElement).click(falzoni.customer.register.removeAddress);

            $("#AddAddress").html("Adicionar");

            falzoni.customer.register.hideForm();

            falzoni.core.configurations.hideLoading();
        }, 500);
    },

    prepareAddressEdit: function () {
        //Obter valores
        let element = $(this).closest("tr");
        let index = $(element).index();

        falzoni.core.configurations.showLoading();

        setTimeout(function () {

            let postalCode = $(element).find(`input#Addresses_${index}__PostalCode`).val();
            let addressName = $(element).find(`input#Addresses_${index}__AddressName`).val();
            let number = $(element).find(`input#Addresses_${index}__Number`).val();
            let complement = $(element).find(`input#Addresses_${index}__Complement`).val();
            let neighborhood = $(element).find(`input#Addresses_${index}__Neighborhood`).val();
            let city = $(element).find(`input#Addresses_${index}__City`).val();
            let estado = $(element).find(`input#Addresses_${index}__State`).val();

            // Save index's table
            $("#IndexTable").val(index);

            $("#Postal").val(postalCode);
            $("#Street").val(addressName);
            $("#Num").val(number);
            $("#Comp").val(complement);
            $("#Region").val(neighborhood);
            $("#District").val(city);
            $("#UF").val(estado);

            $("#AddAddress").html("Salvar");

            $("#AddAddress").off("click", falzoni.customer.register.addAddress);
            $("#AddAddress").on("click", falzoni.customer.register.saveEditAddress);

            falzoni.customer.register.showForm();

            falzoni.core.configurations.hideLoading();
        }, 500);
    },

    saveEditAddress: function () {
        falzoni.core.configurations.showLoading();
        setTimeout(function () {
            let tr = $("#CustomerAddressTable tbody:last > tr");
            let index = $("#IndexTable").val();

            let linha = $(tr).eq(index);

            //Obter valores
            let postalCode = $("#Postal").val();
            let addressName = $("#Street").val();
            let number = $("#Num").val();
            let complement = $("#Comp").val();
            let neighborhood = $("#Region").val();
            let city = $("#District").val();
            let estado = $("#UF").val();

            if (postalCode == '' || addressName == '' || number == '' || neighborhood == '' || city == '' || estado == '') {
                falzoni.core.configurations.hideLoading();
                return;
            }

            //Gravar valores na tabela
            $(linha).find(`input#Addresses_${index}__PostalCode`).parent().html(`<input id="Addresses_${index}__PostalCode" name="Addresses[${index}].PostalCode" type="hidden" value="${postalCode}">${postalCode}`);
            $(linha).find(`input#Addresses_${index}__AddressName`).parent().html(`<input id="Addresses_${index}__AddressName" name="Addresses[${index}].AddressName" type="hidden" value="${addressName}">${addressName}`);
            $(linha).find(`input#Addresses_${index}__Number`).parent().html(`<input id="Addresses_${index}__Number" name="Addresses[${index}].Number" type="hidden" value="${number}">${number}`);
            $(linha).find(`input#Addresses_${index}__Complement`).parent().html(`<input id="Addresses_${index}__Complement" name="Addresses[${index}].Complement" type="hidden" value="${complement}">${complement}`);
            $(linha).find(`input#Addresses_${index}__Neighborhood`).parent().html(`<input id="Addresses_${index}__Neighborhood" name="Addresses[${index}].Neighborhood" type="hidden" value="${neighborhood}">${neighborhood}`);
            $(linha).find(`input#Addresses_${index}__City`).parent().html(`<input id="Addresses_${index}__City" name="Addresses[${index}].City" type="hidden" value="${city}">${city}`);
            $(linha).find(`input#Addresses_${index}__State`).parent().html(`<input id="Addresses_${index}__State" name="Addresses[${index}].State" type="hidden" value="${estado}">${estado}`);

            //Resetar botão e input hidden do index
            $("#AddAddress").html("Adicionar");

            falzoni.customer.register.clearAddressForm();
            falzoni.customer.register.hideForm();

            $("#AddAddress").on("click", falzoni.customer.register.addAddress);
            $("#AddAddress").off("click", falzoni.customer.register.saveEditAddress);

            falzoni.core.configurations.hideLoading();
        }, 500)
    },

    removeAddress: function () {
        falzoni.core.configurations.showLoading();

        let element = $(this).closest("tr");

        setTimeout(function () {

            if ((element).find("[id*=Removed]").length) {
                $(element).find("[id*=Removed]").val("true");
                $(element).hide();
            } else {
                $(element).remove();
                falzoni.customer.register.reloadTableIndex();
            }

            // Verificar se tabela possui algum elemento ou se todos estão visíveis
            if ($("#CustomerAddressTable tbody:last > tr").length <= 0 || !$("#CustomerAddressTable tbody:last > tr").is(':visible')) {
                falzoni.customer.register.removeTable();
                falzoni.customer.register.addRegisterMessage();
                falzoni.customer.register.hideForm();
            }

            falzoni.core.configurations.hideLoading();
        }, 500)
    },

    reloadTableIndex: function () {
        $("#CustomerAddressTable tbody:last tr").each(function () {
            let index = $(this).index();

            //Realinhar índices
            $(this).find("[id*=Id]").attr("id", `Addresses_${index}__Id`).attr("name", `Addresses[${index}].Id`);
            $(this).find("[id*=PostalCode]").attr("id", `Addresses_${index}__PostalCode`).attr("name", `Addresses[${index}].PostalCode`);
            $(this).find("[id*=AddressName]").attr("id", `Addresses_${index}__AddressName`).attr("name", `Addresses[${index}].AddressName`);
            $(this).find("[id*=Number]").attr("id", `Addresses_${index}__Number`).attr("name", `Addresses[${index}].Number`);
            $(this).find("[id*=Complement]").attr("id", `Addresses_${index}__Complement`).attr("name", `Addresses[${index}].Complement`);
            $(this).find("[id*=Neighborhood]").attr("id", `Addresses_${index}__Neighborhood`).attr("name", `Addresses[${index}].Neighborhood`);
            $(this).find("[id*=City]").attr("id", `Addresses_${index}__City`).attr("name", `Addresses[${index}].City`);
            $(this).find("[id*=State]").attr("id", `Addresses_${index}__State`).attr("name", `Addresses[${index}].State`);
        });
    },

    //Buscar Cep na api dos Correios
    searchCep: function () {
        let postalCode = $(this).val();

        if (postalCode == '' || postalCode.length < 9) return;

        $.ajax({
            type: 'GET',
            url: `https://viacep.com.br/ws/${$(this).val()}/json/`,
            dataType: "json",
            beforeSend: function () {
                falzoni.core.configurations.showLoading();
            },
            success: function (data) {
                $("#Postal").val(data.cep);
                $("#Street").val(data.logradouro);
                $("#Comp").val(data.complemento);
                $("#Region").val(data.bairro);
                $("#District").val(data.localidade);
                $("#UF").val(data.uf);

                falzoni.core.configurations.hideLoading();
            },
            error: function (request, status, error) {
                console.error(request);
                console.error(status);
                console.error(error);
                //falzoni.core.configurations.modais.configurarModalErro("Erro ao buscar CEP!", "Verifique o CEP digitado e tente novamente");
                falzoni.core.configurations.alerts.configAlert("Erro ao buscar CEP", "alert-danger");
                falzoni.core.configurations.hideLoading();
            }
        })
    },

    validateForm: function () {
        if ($("#CustomerAddressTable tbody:last > tr").length <= 0) {
            falzoni.core.configurations.alerts.configAlert("É necessário incluir ao menos um endereço", "alert-danger");
            return false;
        }

        falzoni.core.configurations.showLoading();
    }
}

// Register components
$(document).ready(falzoni.customer.register.registerComponents());