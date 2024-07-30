//Inicializar classes
var falzoni = falzoni || {};
falzoni.user = falzoni.user || {};
falzoni.customer = falzoni.customer || {};

falzoni.core = falzoni.core || {
    configurations: {
        modals: {
            configModalSuccess: function (mensagem, url) {
                $(".modal-success").modal({
                    backdrop: 'static',
                    keyboard: false
                });

                $(".modal-success").find("h1").html(mensagem);
                $(".modal-success").find("a").attr("href", url);
            },

            configModalWarning: function (mensagem, callbackFunction) {
                $(".modal-warning").modal({
                    backdrop: 'static',
                    keyboard: false
                });

                $(".modal-warning").find("h1").html(mensagem);

                // Limpar eventos
                $(".modal-warning").find("#Confirmar").unbind();
                $(".modal-warning").find("#Confirmar").click(callbackFunction);
            },

            configModalError: function (title, error) {
                $(".modal-danger").modal({
                    backdrop: 'static',
                    keyboard: false
                });

                $(".modal-danger").find("h3").html(title);
                $(".modal-danger").find("h4").html(error);
            },

            configModalListError: function (errors) {
                $(".modal-danger").modal({
                    backdrop: 'static',
                    keyboard: false
                });

                for (var i = 0; i < errors.length; i++) {
                    $(".modal-danger").find("ul").append("<li><h3>" + errors[i] + "</h3></li>");
                }
            },
        },

        alerts: {
            configAlert: function (message, alertType) {
                // Exibir mensagem enviada por parâmetro
                $(".alert-jquery").find("p.text-alert-jquery").html(message);

                falzoni.core.configurations.alerts.removeAlerts();
                falzoni.core.configurations.alerts.includeAlertClass(alertType);
                falzoni.core.configurations.alerts.showAlert();
                
            },

            showAlert: function () {
                //Remover classe de segurança d-none e efeito de fade in
                if ($(".alert-jquery").hasClass("d-none")) {
                    $(".alert-jquery").hide();
                    $(".alert-jquery").removeClass("d-none");
                }

                // Setar tempo de 5 segundos para alert desaparecer
                setTimeout(function () {
                    $(".alert-jquery").fadeOut();
                }, 5000);

                $(".alert-jquery").fadeIn();
            },

            includeAlertClass: function (alertType) {
                if (!$(".alert-jquery").hasClass(alertType)) {
                    $(".alert-jquery").addClass(alertType);
                }
            },

            removeAlerts: function () {
                //Remover quaisquer outras classes de alertas que estiverem
                if ($(".alert-jquery").hasClass("alert-warning")) {
                    $(".alert-jquery").removeClass("alert-warning");
                }

                if ($(".alert-jquery").hasClass("alert-danger")) {
                    $(".alert-jquery").removeClass("alert-danger");
                }

                if ($(".alert-jquery").hasClass("alert-success")) {
                    $(".alert-jquery").removeClass("alert-success");
                }

                if ($(".alert-jquery").hasClass("alert-warning")) {
                    $(".alert-jquery").removeClass("alert-warning");
                }

                if ($(".alert-jquery").hasClass("alert-info")) {
                    $(".alert-jquery").removeClass("alert-info");
                }
            }
        },

        autoLoad: function () {
            $(".modal-danger").on("hidden.bs.modal", function () {
                $(this).find("ul").empty();
            });

            $(".alert-close").click(function () {
                $(this).parent().fadeOut();
            });

            setTimeout(function () {
                $(".alert-back").fadeOut();
            }, 5000);
        },

        showLoading: function () {
            //$(".loading").show();
            $(".loading").fadeIn();
        },

        hideLoading: function () {
            //$(".loading").hide();
            $(".loading").fadeOut();
        }
    }
};

$(document).ready()