falzoni.core.datepicker = falzoni.core.datepicker || {
    registerConfigurations: function () {
        $(".input-group.date").datepicker({
            format: "dd/mm/yyyy",
            language: "pt-BR",
            autoclose: true,
        });
    }
    
}