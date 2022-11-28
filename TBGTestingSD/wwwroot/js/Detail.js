var _id = $("#inputId").val(),
    _dataHeader,
    _dataDetail,
    _typeForm = 'add',
    _typeMaterial;
$(document).ready(function () {
    control.init();
    submit.init();
})

var control = {
    init: function () {
        control.getHeader(control.setInputDate);
        window.setInterval(control.bindExistData(), 500);
        $('#tglDiterima').datepicker({ format: 'yyyy-mm-dd' });
        $("#tipeMaterial").on('change', function () {
            var Type = $(this).val();
            control.getMaterial(Type);
        });
    },
    getHeader: function (callbackFn) {
        $.ajax({
            type: 'get',
            url: '/Header/GetById?id=' + _id,
            cache: false,
            success: function (obj, textStatus) {
                _dataHeader = obj;
                callbackFn();
            },
            error: function (obj, textStatus) {
                alert('error');
            }
        })
    },
    getMaterial: function (type) {
        if (type != '') {
            $.ajax({
                type: 'get',
                url: '/Detail/GetMaterial?tipe=' + type,
                cache: false,
                success: function (obj, textStatus) {
                    console.log(obj);
                    $('#material').text('');
                    $('#material').prop('disabled', false);
                    $('#material').prop('required', true);
                    $('#material').append('<option value="">pilih material..</option>');
                    var strHtml = '';
                    $.each(obj, function (k, v) {
                        strHtml = '<option value=' + v.id + '>' + v.material + '</option>'
                        $('#material').append(strHtml);
                    })
                },
                error: function (obj, textStatus) {
                    alert('error');
                }
            });
        } else {
            $('#material').text('');
            $('#material').prop('disabled', true);
            $('#material').prop('required', false);
        }
    },
    setInputDate: function () {
        var dateSPR = new Date(_dataHeader.tanggalMinta);
        dateSPR.setDate(dateSPR.getDate() + 14);
        console.log(dateSPR);
        console.log(new Date(dateSPR));
        var result = dateSPR.toLocaleString().split('T')[0];
        console.log(result);
        $('#tglDiterima').datepicker('setDate', new Date(result));
    },
    bindExistData: function () {
        $.ajax({
            type: 'get',
            url: '/Detail/GetDetailByIdHeader?id=' + _id,
            cache: false,
            success: function (obj, textStatus) {
                console.log(obj);
                if (obj) {
                    _typeForm = 'update';
                    _dataDetail = obj.detilSpr;
                    _typeMaterial = obj.tipeMaterial;
                    form.init();
                }
            },
            error: function (obj, textStatus) {
                alert('error');
            }
        })
    }
}

var form = {
    init: function () {
        $("#btnChange").off().on('click', function () {
            form.btnChange();
        });
        $("#btnCancel").off().on('click', function () {
            form.btnCancel();
        });
        form.btnCancel();  
    },
    update: function () {
        var obj = _dataDetail;
        $('#tipeMaterial').val((_typeMaterial) ? 1 : 0).change();
        $('#volume').val(obj.volume);
        $('#unit').val(obj.unit);
        $('#statusDisetujui').val((obj.statusDisetujui) ? 1 : 0).change();
        var date = new Date(obj.tanggalRencanaDiterima).toLocaleString();
        console.log(date);
        console.log(new Date(obj.tanggalRencanaDiterima).toLocaleString());
        $('#tglDiterima').datepicker('setDate', new Date(date));
        window.setTimeout(function () {
            $('#material').val(obj.material).change()
            $('.toogle').prop('disabled', true);
        },500)
    },
    btnChange: function () {
        $('.toogle').prop('disabled', false);
        $("#btnChange").prop('hidden', true);
        $("#btnBack").prop('hidden', true);
        $("#btnSave").prop('hidden', false);
        $("#btnCancel").prop('hidden', false);
    },
    btnCancel: function () {
        form.update();
        $("#btnChange").prop('hidden', false);
        $("#btnBack").prop('hidden', false);
        $("#btnSave").prop('hidden', true);
        $("#btnCancel").prop('hidden', true);
    },
}

var submit = {
    init: function () {
        $("#formDetail").submit(function (e) {
            e.preventDefault();
            if (_typeForm == 'add') {
                submit.add();
            } else {
                submit.update();
            }
        })
    },
    add: function () {
        var params = new Object();
        params.IdRef = _id;
        params.Material = $("#material").val();
        params.Volume = $("#volume").val();
        params.Unit = $("#unit").val();
        params.StatusDisetujui = ($("#statusDisetujui").val() == 1);
        params.TanggalRencanaDiterima = $("#tglDiterima").val();

        $.ajax({
            type: 'post',
            url: '/Detail/AddNew',
            data: params,
            cache: false,
            success(data) {
                if (data == 'pass') {
                    alert('success');
                    window.location.reload();
                } else {
                    alert(data.message);
                }
            },
            error(data) {
                alert('error')
            }
        })
    },
    update: function () {
        var params = new Object();
        params.Id = _dataDetail.id;
        params.IdRef = _id;
        params.Material = $("#material").val();
        params.Volume = $("#volume").val();
        params.Unit = $("#unit").val();
        params.StatusDisetujui = ($("#StatusDisetujui").val() == 1);
        params.TanggalRencanaDiterima = $("#tglDiterima").val();

        $.ajax({
            type: 'post',
            url: '/Detail/Update',
            data: params,
            cache: false,
            success(data) {
                if (data == 'pass') {
                    alert('success');
                    window.location.reload();
                } else {
                    alert(data.message);
                }
            },
            error() {
                alert('error')
            }
        })
    }
}