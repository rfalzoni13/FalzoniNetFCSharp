falzoni.core.imageUtility = falzoni.core.imageUtility || {
    clickPhotoButton: function (element, photoElement, type) {
        if (element.val().length > 0) {
            cancelCut(element, photoElement, type);
        }

        element.click();
    },

    cancelCut: function (element, photoElement, type) {
        element.val("");

        if(type == 'user')
            photoElement.attr("src", "/Content/Images/Profile/user.png");
    },

    changePhoto: function (e, input, element, modalElement) {
        var target = e.target || e.srcElement;
        if (target.value.length === 0) {
            return null;
        }
        modalElement.modal({
            backdrop: 'static',
            keyboard: false
        });
        falzoni.core.imageUtility.loadPhoto(input, element);
    },

    loadPhoto: function (input, output) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                output.attr('src', e.target.result);
            };

            reader.readAsDataURL(input.files[0]);
            //return input.files[0].name;
        }
    },

    adjustImage: function (element) {
        var minCroppedWidth = 320;
        var minCroppedHeight = 320;
        var maxCroppedWidth = 2048;
        var maxCroppedHeight = 2048;
        element.cropper({
            autoCropArea: 0.5,
            viewMode: 2,
            responsive: true,
            data: {
                width: (minCroppedWidth + maxCroppedWidth) / 2,
                height: (minCroppedHeight + maxCroppedHeight) / 2
            },
            crop: function (event) {

                var width = event.detail.width;
                var height = event.detail.height;

                if (
                    width < minCroppedWidth
                    || height < minCroppedHeight
                    || width > maxCroppedWidth
                    || height > maxCroppedHeight
                ) {
                    falzoni.core.imageUtility.defineCropperImage(Math.max(minCroppedWidth, Math.min(maxCroppedWidth, width)), Math.max(minCroppedHeight, Math.min(maxCroppedHeight, height)), element);
                }

                //console.log("Posição X é: " + event.detail.x);
                //console.log("Posição Y é: " + event.detail.y);
                //console.log("Largura é: " + event.detail.width);
                //console.log("Altura é: " + event.detail.height);
                //console.log("Rotação é: " + event.detail.rotate);
                //console.log("Escala X é: " + event.detail.scaleX);
                //console.log("Escala Y é: " + event.detail.scaleY);
            }
        });
    },

    defineCropperImage: function (w, h, element) {
        var cropper = element.data('cropper');
        cropper.setData({
            width: w,
            height: h
        });
    },

    cutImage: function (obj) {
        var cropper = obj.element.data('cropper');
        var canvas = cropper.getCroppedCanvas();
        obj.photoElement.attr("src", canvas.toDataURL());
        //falzoni.core.imageUtility.prepareImage(cropper, out);
        falzoni.core.imageUtility.prepareImage(obj.input, obj.outFileName, obj.outBase64, obj.form, canvas);
        $(".modal").modal("hide");
    },

    prepareImage: function (input, outFileName, outBase64, form, canvas) {
        var file = input[0].files[0]
        if (file == null || file == undefined)
            return;

        let inputFileName = '<input type="hidden" name="' + outFileName.replace('-', '.') + '" id="' + outFileName.replace('-', '_') + '" value="' + file.name + '">';
        let inputBase64 = '<input type="hidden" name="' + outBase64.replace('-', '.') + '" id="' + outBase64.replace('-', '_') + '" value="' + canvas.toDataURL(file.type) + '">';
                
        form.append(inputFileName);
        form.append(inputBase64);
    }

    //prepareImage: function (cropper, formData, name) {
    //    cropper.getCroppedCanvas().toBlob((blob) => {

    //        // Pass the image file name as the third parameter if necessary.
    //        formData.append(name, blob);

    //    });
    //},/*, 'image/png' */
}