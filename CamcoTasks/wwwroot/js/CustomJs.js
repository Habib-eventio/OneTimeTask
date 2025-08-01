window.modalDraggable = function () {
    $('.modal-dialog').draggable({
        handle: ".modal-header"
    });

    $('#TaskUpdatesModal').modal('hide');
}

function AddRedBox(Id) {
    var element = document.getElementById(Id);

    if (element !== null) {
        element.style.border = "3px solid #FF0404";
    } else {
        console.error("Element with ID '" + Id + "' not found.");
    }
}

function RemoveRedBox(Id) {
    document.getElementById(Id).style.border = "1px solid #CED4DA";
}

function HideModal(ModalId) {
    $(ModalId).modal('hide')
}


function HideModalWithModalBackdrop(ModalId) {
    $(ModalId).modal('hide')
    $('body').removeClass('modal-open');
    $('.modal-backdrop').remove();
}

function HideAllModalWithModalBackdrop() {
    const modals = document.querySelectorAll('.modal');

    modals.forEach(modal => {
        modal.style.display = 'none';

    });

    $('body').removeClass('modal-open');
    $('.modal-backdrop').remove();
}


function ShowModal(ModalId) {
    $(ModalId).modal('show')
}

function Print(Path) {
    var page = window.open(Path);
    page.print();
}

function SetZIndex(ModalId, IndexValue) {
    if (IndexValue == null || IndexValue == undefined)
        IndexValue = '';
    $(ModalId).css('z-index', IndexValue);
}

window.blazorOpen = (args) => {
    window.open(args);
};

function SetUpSelected(RadioId) {
    console.log(RadioId);
    radiobtn = document.getElementById(RadioId);
    radiobtn.checked = true;
}

function GetChartImage() {
    //var ImageData = document.getElementById('ChartDiv').toDataURL();
    let CaptureDiv = document.getElementById('ChartDiv');
    html2canvas(CaptureDiv).then(
        function (canvas) {
            document
                .getElementById('storedScreenshot')
                .appendChild(canvas);
        });
}

function downloadFile(filename, fileUrl) {
    var a = document.createElement("a");
    a.href = fileUrl;
    a.setAttribute("download", filename);
    a.click();
}

function ImagetoPrint(source) {
    return "<html><head><scri" + "pt>function step1(){\n" +
        "setTimeout('step2()', 10);}\n" +
        "function step2(){window.print();window.close()}\n" +
        "</scri" + "pt></head><body onload='step1()'>\n" +
        "<img src='" + source + "' /></body></html>";
}

function DivtoPrint(source) {
    return "<html><head><scri" + "pt>function step1(){\n" +
        "setTimeout('step2()', 10);}\n" +
        "function step2(){window.print();window.close()}\n" +
        "</scri" + "pt></head><body onload='step1()'>\n" +
        + source + "</body></html>";
}

function PrintImage(source) {
    var Pagelink = "about:blank";
    var pwa = window.open(Pagelink, "_new");
    pwa.document.open();
    pwa.document.write(ImagetoPrint(source));
    pwa.document.close();
}

function PrintDiv(source) {
    var Pagelink = "about:blank";
    var pwa = window.open(Pagelink, "_new");
    pwa.document.open();
    pwa.document.write(DivtoPrint(source));
    pwa.document.close();
}

function StartprintDiv(DivId) {

    var params = [
        'height=' + screen.height,
        'width=' + screen.width,
        'fullscreen=yes' // only works in IE, but here for completeness
    ].join(',');

    var mywindow = window.open('', 'PRINT', params);

    mywindow.document.write('<html><head><title>' + document.title + '</title>');
    mywindow.document.write('<link rel="stylesheet" href="/css/bootstrap/bootstrap.min.css" />');
    mywindow.document.write('<link href="_content/Syncfusion.Blazor/styles/fabric.css" rel="stylesheet" />');
    mywindow.document.write('<link href="_content/Syncfusion.Blazor/styles/bootstrap4.css" rel="stylesheet" />');
    mywindow.document.write('<link href="_content/Blazored.Toast/blazored-toast.min.css" rel="stylesheet" />');

    mywindow.document.write('</head><body >');
    mywindow.document.write('<h1>' + document.title + '</h1>');
    mywindow.document.write(document.getElementById("Reccontainer").innerHTML);
    mywindow.document.write('</body></html>');

    mywindow.document.close(); // necessary for IE >= 10
    mywindow.focus(); // necessary for IE >= 10*/   

    mywindow.print();
    // mywindow.close();

    return true;
}

function jsSaveAsFile(filename, byteBase64) {
    var link = document.createElement('a');
    link.download = filename;
    link.href = "data:application/octet-stream;base64," + byteBase64;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

function SelecElement(id, className) {
    const element = document.getElementById(id);

    if (element != null && element.classList.contains(className)) {
        element.classList.remove(className);
    }
    else {
        element.classList.add(className);
    }

    element.blur();
}

function SelecElement(id, addStyle, removeStyle) {
    const element = document.getElementById(id);

    if (element == null) return;

    if (element.style.backgroundColor == addStyle) {
        element.style.backgroundColor = removeStyle;
    }
    else {
        element.style.backgroundColor = addStyle;
    }
}

function SelectAllColumn(tableId, colName, className) {
    var table = document.getElementById(tableId);

    if (table == null) {
        return;
    }

    var row = table.rows[0];

    for (var i = 0; i < row.cells.length; i++) {
        if (row.cells[i].textContent === colName) {
            for (var j = 0; j < table.rows.length; j++) {
                var cell = table.rows[j].cells[i];
                var input = cell.querySelector('input');

                if (input != null) {
                    if (input.classList.contains(className)) {
                        input.classList.remove(className);
                    }
                    else {
                        input.classList.add(className);
                    }
                }
            }
        }
    }
}

function SelectRow(tableId, rowIndex, className) {
    var table = document.getElementById(tableId);

    if (table == null) {
        return;
    }

    var row = table.rows[rowIndex];

    for (var i = 0; i < row.cells.length; i++) {
        var cell = row.cells[i];
        var input = cell.querySelector('input');

        if (input != null) {
            if (input.classList.contains(className)) {
                input.classList.remove(className);
            }
            else {
                input.classList.add(className);
            }
        }
    }
}

function UnselectAll(className) {
    const elements = document.querySelectorAll(className);
    elements.forEach(element => {
        var removeClass = className;

        if (removeClass.charAt(0) === '.') {
            removeClass = removeClass.substring(1);
        }

        var allClass = element.className;
        var classIndex = allClass.indexOf(removeClass);
        allClass = allClass.substring(0, classIndex);
        element.className = allClass;
    });
}

function GetScreenRegulations() {
    var width = window.screen.width;
    var height = window.screen.height;

    return { width: width, height: height };
}

async function TakeScreenshotAndCopy(sourceDivId) {
    let base64Image = "";
    const element = document.getElementById(sourceDivId);

    if (element == null) {
        return base64Image;
    }

    var canvas = await html2canvas(element);

    if (canvas == null) {
        return base64Image;
    }

    base64Image = canvas.toDataURL('image/png');

    return base64Image;
}
function downloadFile(fileName, content) {
    const link = document.createElement("a");
    link.download = fileName;
    link.href = content;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}
window.localStorageHelper = {
    setItem: function (key, value) {
        localStorage.setItem(key, value);
    },
    getItem: function (key) {
        return localStorage.getItem(key);
    }
};

document.addEventListener("DOMContentLoaded", function () {
    const sidebar = document.querySelector(".sidebar-scroll");

    if (sidebar) {
        sidebar.addEventListener("wheel", function (event) {


            const canScrollUp = sidebar.scrollTop > 0;
            const canScrollDown = sidebar.scrollTop + sidebar.clientHeight < sidebar.scrollHeight;

            if ((event.deltaY < 0 && canScrollUp) || (event.deltaY > 0 && canScrollDown)) {
              
                return;
            }
            event.preventDefault();
            event.stopPropagation();
        }, { passive: false });
    }
});
function openTaskTypeModal() {
    var modal = new bootstrap.Modal(document.getElementById('taskTypeModal'));
    modal.show();
}
