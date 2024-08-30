falzoni.product.register = falzoni.product.register || {
    registerComponents: function () {

        falzoni.core.configurations.autoLoad();

        falzoni.core.datepicker.registerConfigurations();
    },
}

// Register components
$(document).ready(falzoni.product.register.registerComponents());