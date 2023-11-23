$(document).ready(function () {

    $.fn.serializeObject = function () {
        var o = {};
        var a = this.serializeArray();
        $.each(a, function () {
            if (o[this.name]) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        return o;
    };

    var taskRecords = $('#taskList').DataTable({
        "lengthChange": false,
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": 'https://localhost:7263/api/thetask/list',
            "type": "GET",
            dataSrc: ''
        },
        columns: [
            { data: 'id' },
            { data: 'name' },
            { data: 'description' },
            { data: 'created' },
            {
                'data': null, title: 'Action', "render": function (data, type, row, meta)
                {
                    return `<div class="btn-group" id="${row.id}"> 
                    <button type="button" class="btn btn-light edit" id="${row.id}-edit"  >Edit
                    </button> <button type="button" class="btn btn-danger delete"
                    id="${row.id}-delete" >Delete</button></div>`
                }
            },

        ],
        "pageLength": 10,

    });

    $('#addTask').click(function () {
        $('#taskModal').modal('show');
        $('#taskForm')[0].reset();
        $('.modal-title').html("<i class='fa fa-plus'></i> Add Task");
        $('#action').val('addTask');
        $('#save').val('Add');
    });

    function onEdit(row) {
        var id = row.id;
        $.ajax({
            url: 'https://localhost:7263/api/thetask/' + id,
            method: "GET",
            dataType: "json",
            success: function (data) {
                $('#taskModal').modal('show');
                $('#id').val(data.id);
                $('#name').val(data.name);
                $('#description').val(data.description);
                $('.modal-title').html("<i class='fa fa-edit'></i> Edit Task");
                $('#action').val('updateTask');
                $('#save').val('Save');
            }
        })
    }

    $("#taskModal").on('submit', '#taskForm', function (event) {
        event.preventDefault();
        $('#save').attr('disabled', 'disabled');
        var formData = $(this).serializeObject();
        if (!formData.id) {
            $.ajax({
                url: "https://localhost:7263/api/thetask",
                method: "POST",
                contentType: 'application/json',
                data: JSON.stringify(formData),
                dataType: 'json',
                success: function (data) {
                    $('#taskForm')[0].reset();
                    $('#taskModal').modal('hide');
                    $('#save').attr('disabled', false);
                    taskRecords.ajax.reload();
                }
            })
        } else {
            $.ajax({
                url: "https://localhost:7263/api/thetask/"+formData.id,
                method: "PUT",
                contentType: 'application/json',
                data: JSON.stringify(formData),
                dataType: 'json',
                success: function (data) {
                    $('#taskForm')[0].reset();
                    $('#taskModal').modal('hide');
                    $('#save').attr('disabled', false);
                    taskRecords.ajax.reload();
                }
            })
        }
       
    });

    function onDelete(row) {
        var id = row.id;
        if (confirm("Are you sure you want to delete this Task?")) {
            $.ajax({
                url: "https://localhost:7263/api/thetask/" + id,
                method: "DELETE",
                success: function (data) {
                    taskRecords.ajax.reload();
                }
            })
        } else {
            return false;
        }
    }

    $('#taskList tbody').on('click', '.edit', function () {
        var tr = $(this).closest('tr');
        var data = taskRecords.row(tr).data();
        onEdit(data);
    });


    $('#taskList tbody').on('click', '.delete', function () {
        var tr = $(this).closest('tr');
        var data = taskRecords.row(tr).data();
        onDelete(data);

    });



});