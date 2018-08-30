var serverAddress = 'http://168.61.216.55:8888/api/minecraft/'

function deleteServer(serverName) {
    if (confirm('Do you really want to delete "' + serverName + '"?'))
        return $.ajax({
            url: serverAddress + serverName,
            type: 'delete',
            success: data => {
                alert('Delete with success: ', JSON.stringify(data));
            },
            error: data => {
                alert('Error when deleting the server: ', JSON.stringify(data));
            }
        });
}

function createServer() {
    var serverName = $("#server-name").val();
    var postData = {
        name: serverName
    };
    return $.ajax({
        url: serverAddress,
        type: 'post',
        dataType: 'json',
        contentType: 'application/json',
        data: JSON.stringify(postData),
        success: data => {
            alert('Create returned with success: ', JSON.stringify(data));
        },
        error: data => {
            alert('Error when creating the server: ', JSON.stringify(data));
        }
    });
}