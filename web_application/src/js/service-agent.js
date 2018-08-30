var serverAddress = 'http://172.16.150.143:8888/api/minecraft/'

function deleteServer(serverName) {
    return $.ajax({
        url: serverAddress + serverName,
        type: 'delete',
        success: data => {
            console.log('Delete returned with success: ', data);
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
            console.log('Create returned with success: ', data);
        }
    });
}