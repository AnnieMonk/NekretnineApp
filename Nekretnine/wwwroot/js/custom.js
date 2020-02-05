//Ajax za tabelu obilazaka
$(document).ready(function () {
    $.get("/Obilasci/Tabela/",
        function (rezultat, status) {
            $("#rezultatTabele").html(rezultat);
        });
});
//za in field promjenu adrese
$(document).ready(function (parameters) {
    $(".ajaxAdresa").change(function () {
        var adresa = $(this).val();
        var ObilID = $(this).attr('data-recordid');
        var urlZaPoziv = "/Obilasci/UrediAdresu?ObilazakID=" + ObilID + "&adresa=" + adresa;

        $.ajax({
            type: "POST",
            url: urlZaPoziv,
            //data: form.serialize(),//??
            success: function (data) {
                $(".ajaxAdresa").html(data);
            }
        });
    });

});
$(document).ready(function (parameters) {
    $(".ajaxDatum").change(function () {
        var datum = $(this).val();
        var ObilID = $(this).attr('data-recordid');
        var urlZaPoziv = "/Obilasci/UrediDatum?ObilazakID=" + ObilID + "&datum=" + datum;

        $.ajax({
            type: "POST",
            url: urlZaPoziv,
            //data: form.serialize(),//??
            success: function (data) {
                $(".ajaxDatum").html(data);
            }
        });
    });

});
$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip()
});


//za PDF
$(document).ready(function () {
    $('.custom-file-input').on("change", function () {
        var fileName = $(this).val().split("\\").pop();
        $(this).next('.custom-file-label').html(fileName);
    });
});

//za modal
$(document).ready(function ShowPopup(title, body) {
    $("#uplataModal .modal-title").html(title);
    $("#uplataModal .modal-body").html(body);
    $("#uplataModal").modal("show");
});

//za sort, search
$(document).ready(function () {
    $('#dataTable').DataTable();
});

//za brisanje slike
$(document).ready(function () {
    $('.deleteItem').click(function (e) {
        e.preventDefault();
        var $ctrl = $(this);
        if (confirm('Da li stvarno želite obrisati?')) {
            $.ajax({
                url: "/Nekretnina/DeleteFile",
                type: 'DELETE',
                data: { id: $(this).data('id') },
                success: function (data) {
                    $("#rezultatBrisanja").html(data);
                }
            }).done(function (data) {
                if (data.Result == "OK") {
                    $ctrl.closest('li').remove();
                }
                else if (data.Result.Message) {
                    alert(data.Result.Message);
                }

            }).fail(function () {
                alert("Greška. Pokušajte ponovo");
            })

        }
    });
});