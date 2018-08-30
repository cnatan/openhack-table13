var serverAddress = 'http://168.61.216.55:8888/api/minecraft/'

$.ajaxSetup({
    beforeSend: function () {
        $('#loader').show();
    },
    complete: function () {
        $('#loader').hide();
    },
    success: function () {}
});

function loadServerTable(should_include_action = false) {
    const response = $.ajax(serverAddress);
    response.then(data => {
        data.forEach(element => {
            let html = '';
            if (should_include_action === true) {
                html = '<td><a href="#" onClick="javascript:deleteServer(\'' + element.name + '\')">Delete this server</a></td>'
            }

            const table = $('.server-table');
            table.append(
                '<tr>',
                '<td>' + element.name + '</td>',
                '<td>' + element.endpoints.minecraft + '</td>',
                '<td>' + element.endpoints.rCon + '</td>',
                html,
                '</tr>'
            );
        });
    });
}