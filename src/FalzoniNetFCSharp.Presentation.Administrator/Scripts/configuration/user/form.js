falzoni.user.register = falzoni.user.register || {
    registerComponents: function () {
        $("#BtnPhoto").click(falzoni.user.register.clickPhotoButton);
        $("#FileProfile").change(falzoni.user.register.changePhoto);
        $("#CutImage").click(falzoni.user.register.cutImage);
        $(".cancel-cut").click(falzoni.user.register.cancelCut);
        $(".modal-photo").on("show.bs.modal", function () {
            setTimeout(function () {
                falzoni.user.register.adjustImage();
            }, 500)
        });
        $(".modal-photo").on("hidden.bs.modal", function () {
            $("#ImagePreview").removeAttr("src").removeAttr("style").cropper("destroy");
        });

        $("#Roles").select2();

        $(".form-user").submit(falzoni.core.configurations.showLoading);

        falzoni.core.configurations.autoLoad();

        falzoni.core.datepicker.registerConfigurations();
    },

    clickPhotoButton: function () {
        falzoni.core.imageUtility.clickPhotoButton($("#FileProfile"), $("#PhotoImg"), 'user');
    },

    cancelCut: function () {
        falzoni.core.imageUtility.cancelCut($("#FileProfile"), $("#PhotoImg"), 'user');
    },

    changePhoto: function (e) {
        falzoni.core.imageUtility.changePhoto(e, this, $("#ImagePreview"), $(".modal-photo"));
    },

    adjustImage: function () {
        falzoni.core.imageUtility.adjustImage($("#ImagePreview"));
    },

    cutImage: function () {
        let obj = {
            element: $("#ImagePreview"),
            photoElement: $("#PhotoImg"),
            input: $("#FileProfile"),
            outFileName: 'File-FileName',
            outBase64: 'File-Base64String',
            form: $(".form-user")
        }

        falzoni.core.imageUtility.cutImage(obj);
    },
};

$(document).ready(falzoni.user.register.registerComponents());

