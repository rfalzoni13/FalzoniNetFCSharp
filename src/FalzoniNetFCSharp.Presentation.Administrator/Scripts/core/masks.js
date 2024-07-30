//Aplicação de máscaras
falzoni.masks = {
    registerMasks: function () {
        $(".phone").mask("(00) 0000-0000")
        $(".cellphone").mask("(00) 00000-0000")
        $(".postalcode").mask("00000-000")
        $(".cpf").mask("000.000.000-00")
    }
}

$(document).ready(falzoni.masks.registerMasks());