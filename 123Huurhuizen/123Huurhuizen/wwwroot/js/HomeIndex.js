$(document).ready(function () {
    $(".deleteButton").on("click", function () {
        var $this = $(this);
        var houseId = $this.data("house-id"); 
        Swal.fire({
            icon: 'warning',
            title: 'Weet je zeker dat je dit huis wilt verwijderen?',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6',
            confirmButtonText: 'Ja, verwijderen',
            cancelButtonText: 'Annuleren'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: "/Home/DeleteHouse",
                    type: "POST",
                    data: { houseId: houseId },
                    success: function (response) {
                        if (response.success) {
                            Swal.fire({
                                icon: 'success',
                                title: 'Succesvol verwijderd',
                                showConfirmButton: false,
                                timer: 1500
                            });

                            $this.closest(".col-md-3").remove();
                        } else {

                            Swal.fire({
                                icon: 'error',
                                title: 'Er ging iets mis',
                                showConfirmButton: false,
                                timer: 1500
                            });
                        }
                    },
                    error: function () {
                        Swal.fire({
                            icon: 'error',
                            title: 'Er is een fout opgetreden bij het verwijderen',
                            showConfirmButton: false,
                            timer: 1500
                        });
                    }
                });
            }
        });
    });
});
$(document).ready(function () {
    $(".editButton").on("click", function (event) {
        event.preventDefault(); // Prevent the default action

        var $this = $(this);
        var $card = $this.closest(".card");
        var $editForm = $card.find(".editForm");
        var $eigenHuisButton = $card.find(".EigenhuisButton");

        // Hide all other edit forms
        $(".editForm").not($editForm).hide();

        // Hide all other EigenhuisButtons within the same card
        $card.find(".EigenhuisButton").not($eigenHuisButton).hide();

        // Toggle this edit form
        $editForm.toggle();
        // Toggle this EigenhuisButton
        $eigenHuisButton.toggle();
    });

    $(".saveButton").on("click", function () {
        var $this = $(this);
        var $editForm = $this.closest(".editForm");
        var houseId = $editForm.find("input[name='houseId']").val();
        var rentPerMonth = $editForm.find("input[name='rentPerMonth']").val();
        var availableAt = $editForm.find("input[name='availableAt']").val();

        // AJAX request to update data
        $.ajax({
            url: "/Home/UpdateHouse",
            type: "POST",
            data: {
                houseId: houseId,
                rentPerMonth: rentPerMonth,
                availableAt: availableAt
            },
            success: function (response) {
                if (response.success) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Succesvol aangepast',
                        showConfirmButton: false,
                        timer: 1500
                    });
                    setTimeout(function () {
                        location.reload();
                    }, 1500); // Wacht 1500 milliseconden (1,5 seconde) voordat de pagina wordt herladen
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Er ging iets mis bij het aanpassen',
                        showConfirmButton: false,
                        timer: 1500
                    });
                }
            },
            error: function () {
                Swal.fire({
                    icon: 'error',
                    title: 'Er is een onbekende fout opgetreden',
                    showConfirmButton: false,
                    timer: 1500
                });
            }
        });
    });
});