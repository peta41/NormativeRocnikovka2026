$(function () {

    $("body").on("click", ".person-select", function (event) {
        // only clicked element
        var element = event.target;

        // get guid of element
        var guid = element.getAttribute("data-guid");

        // show suggestion
        $("#" + guid + "-suggestion").removeClass("d-none");


        // hide popop window if user click somewhere in document
        var parentElement = $(element).parent();
        $(document).on('click', function (event) {
            var clickedElement = $(event.target);
            if (!clickedElement.closest(parentElement).length) {
                $("#" + guid + "-suggestion").addClass("d-none");
            }
        });


        // if press esc hide suggestion
        element.addEventListener("keyup", (event) => {
            if (event.key === "Escape") {
                SearchPeopleValid(element);
                // hide if pressed esc
                $("#" + guid + "-suggestion").addClass("d-none");


            } else {
                // filter input by press key
                SearchPeopleSuggestion(element);
            }
        });



    });






});

function SearchPeopleValid(obj) {
    console.log("valid", obj)
    var el = $(obj);
    var guid = el.data("guid");

    var li = $("#" + guid + "-list").children();
    if (!$("#" + guid + "-visible").is(":focus")) {
        $("#" + guid + "-suggestion").addClass("d-none");
    }

    var reset = true;
    var id = 0;
    for (let i = 0; i < li.length; i++) {
        if ($(obj).val().toUpperCase() == $(li[i]).text().toUpperCase()) {
            reset = false;
            id = $(li[i]).data("id");
            break;
        }
    }
    if (reset == true) {
        $(obj).val("");
        $("#" + guid + "-hidden").val("");
    } else {
        $("#" + guid + "-hidden").val(id);
    }

    // if filter is empty show all items
    if (obj.value == "") {
        li.each(function (index) {
            li[index].style.display = "";
        });
    }

}

function personselector(obj) {
    var el = $(obj);
    var id = el.data("id");
    var guid = $(el).parent().attr('id').replace("-list", "");
    $("#" + guid + "-visible").val(el.text().trim());
    $("#" + guid + "-hidden").val(id);
    $("#" + guid + "-suggestion").addClass("d-none");
}


function SearchPeopleSuggestion(obj) {
    var guid = obj.getAttribute("data-guid");
    
    var filter = obj.value.toUpperCase();
    var li = $("#" + guid + "-list").children();

    
    li.each(function (index) {
        if ($(this).text().toUpperCase().includes(filter)) {
            li[index].style.display = "";
        } else {
            li[index].style.display = "none";
        }
    });
}

