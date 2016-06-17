$(document).ready(function() {
        $('#ReleaseDate').datepicker(
            /*$.datepicker.regional["fr"] */
     { showButtonPanel: true });
        $.validator.methods.date = function (value, element) {
            return this.optional(element) || (value.length > 0 && !/Invalid|NaN/.test(new Date(value.replace(/(\d{2}).(\d{2}).(\d{4})/, "$2/$1/$3"))));
        }
        

    }
    
);