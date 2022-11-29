var _typeForm = "";
var _dataList = [];
/*var js = jQuery.noConflict(true);*/

$(document).ready(function () {
    filter.init();
    form.init();
    submit.init();
    table.header();
});
var form = {
    init: function () {
        $("#addButton").on("click", function () {
            _typeForm = "add";
            form.clearAll();
            form.add();
        });
        $("tbody").on("click", '.updateButton', function () {
            _typeForm = "update";
            var id = $(this).attr('value');
            form.update(id);
        })
    },
    add: function () {
        $("#divKodeSPR").hide()
        $("#ModalLabel").text('Add New');
    },
    update: function (id) {
        $("#divKodeSPR").show();
        $('#kodeSPR').prop('disabled', true);
        var dataRow = _dataList.find(obj => {
            return obj.id == id
        })
        $("#ModalLabel").text('Update');
        $("#kodeSPR").val(dataRow.kodeSpr);
        $("#zona").val(dataRow.lokasiPeminta);
        $("#tujuanSPR").val(dataRow.tujuanSpr);
        $("#peminta").val(dataRow.peminta);
        $("#penyetuju1").val(dataRow.namaPenyetuju1);
        $("#penyetuju2").val(dataRow.namaPenyetuju2);
        $("#status").val((dataRow.status) ? 1 : 0).change();
        $("#btnSave").attr('data-id', dataRow.id);
    },
    clearAll: function () {
        $("#tujuanSPR").val("");
        $("#peminta").val("");
        $("#penyetuju1").val("");
        $("#penyetuju2").val("");
        $("#status").val("1").change();
    }
}

var submit = {
    init: function () {
        $("#formHeader").submit(function (e) {
            if (_typeForm == 'add')
                submit.add();
            else
                submit.update();
            e.preventDefault();
        })
    },
    add: function () {
        var params = new Object();
        params.Peminta = $("#peminta").val(),
            params.LokasiPeminta = $("#zona").val(),
            params.TujuanSpr = $("#tujuanSPR").val(),
            params.NamaPenyetuju1 = $("#penyetuju1").val(),
            params.NamaPenyetuju2 = $("#penyetuju2").val(),
            params.Status = ($("#status").val() == 1),
            params.Proyek = 1283

        $.ajax({
            type: 'post',
            url: 'Header/Save',
            data: params,
            cache: false,
            success(data) {
                if (data == 'pass') {
                    alert('success');
                    window.location.reload();
                } else {
                    alert('failed');
                }
            },
            error() {
                alert('error')
            }
        })
    },
    update: function () {
        var params = new Object();
        params.Peminta = $("#peminta").val(),
            params.LokasiPeminta = $("#zona").val(),
            params.TujuanSpr = $("#tujuanSPR").val(),
            params.NamaPenyetuju1 = $("#penyetuju1").val(),
            params.NamaPenyetuju2 = $("#penyetuju2").val(),
            params.Status = ($("#status").val() == 1),
            params.Proyek = 1283,
            params.Id = $("#btnSave").attr('data-id')

        $.ajax({
            type: 'post',
            url: 'Header/Save',
            data: params,
            cache: false,
            success(data) {
                if (data == 'pass') {
                    alert('success');
                    window.location.reload();
                } else {
                    alert('failed');
                }
            },
            error() {
                alert('error')
            }
        })
    }
}

var table = {
    header: function () {
        $.ajax({
            type: 'get',
            url: 'Header/GetAll',
            data: {},
            async: false,
            type: 'json',
            success: function (obj, textStatus) {
                _dataList = obj;
                table.rebind();
            },
            error: function (obj, textStatus) {
                alert('error')
            }
        })
    },
    rebind: function () {
        var table = $("#tblHeader").dataTable({
            destroy: true,
            searching: false,
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'excelHtml5',
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
                    }
                }
            ],
            data: _dataList,
            columns: [
                { 'data': 'kodeSpr' },
                {
                    'data': 'lokasiPeminta', mRender: function (data, type, full) {
                        return (full.lokasiPeminta == '000') ? 'All' : full.lokasiPeminta;
                    }
                },
                { 'data': 'tujuanSpr' },
                {
                    'data': 'tanggalMinta', mRender: function (data, type, full) {
                        var date = new Date(full.tanggalMinta).toLocaleString().split(',')[0];
                        return date;
                    }
                },
                { 'data': 'peminta' },
                { 'data': 'namaPenyetuju1' },
                { 'data': 'namaPenyetuju2' },
                {
                    'data': 'adaDetil', mRender: function (data, type, full) {
                        return (full.adaDetil) ? 'Ada' : 'Tidak';
                    }
                },
                {
                    'data': 'status', mRender: function (data, type, full) {
                        return (full.status) ? 'Aktif' : 'Tidak aktif';
                    }
                },
                {
                    'data': 'id', mRender: function (data, type, full) {
                        var strHrml = '<a href="#" class="updateButton" value="' + full.id + '" data-toggle="modal" data-target="#headerModal">Ubah</a>';
                        strHrml += '<a href="Detail/Index?id=' + full.id + '"> Detil </a>'
                        return strHrml;
                    }
                }
            ],
            drawCallback: function () {
                $('.dt-button').text('Export to excel').addClass('btn btn-primary');
            }

        })
    },
}

var filter = {
    init: function () {
        filter.getPenyetujuan();
        $("#btnReset").on('click', function () {
            filter.reset();
        });
        $('#formFilter').submit(function (e) {
            filter.search();
            e.preventDefault();
        });
    },
    getPenyetujuan: function () {
        $.ajax({
            type: 'get',
            url: 'Header/GetPenyetujuan',
            cache: false,
            success(data) {
                $("#fPenyetujuan").text('');
                $("#fPenyetujuan").append('<option value="">Semua</option>');
                var strHtml = '';
                for (let i = 0; i < data.length; i++) {
                    strHtml += '<option value="' + data[i] + '">' + data[i] + '</option>';
                }
                $("#fPenyetujuan").append(strHtml);
            },
            error() {
                alert('error');
            }
        });
    },
    reset: function () {
        $('#fPenyetujuan').val('').change();
        $('#fAdaDetil').val('').change();
        $('#fStatus').val('').change();
    },
    search: function () {
        var params = new Object();
        params.Penyetujuan = $("#fPenyetujuan").val();
        params.AdaDetil = $("#fAdaDetil").val();
        params.Status = $("#fStatus").val();

        $.ajax({
            type: 'get',
            url: 'Header/GetAll',
            data: params,
            async: false,
            type: 'json',
            success: function (obj, textStatus) {
                _dataList = obj;
                console.log(_dataList);
                table.rebind();
            },
            error: function (obj, textStatus) {
                alert('error')
            }
        })
    }
}