




document.getElementById("sendBtn").addEventListener("click", () => {

    const Name = document.getElementById('username').value;
    const MessageTitle = document.getElementById('MessageTitle').value;
    const MessageBody = document.getElementById('message').value;

    const GetLoginPreviewUrl = `${window.location.origin}/Chat/CreateMessage?Title=${MessageTitle}&Body=${MessageBody}&Name=${sender}&Reciver=${Name}`;
     
    fetch(GetLoginPreviewUrl).then(function () { 
    });

});

var ReciverInput = document.getElementById("username"); 
ReciverInput.onchange = function () {

    let navbar = document.getElementById('History');
    const GetChatHistory = `${window.location.origin}/Chat/ChatHistory?Sender=${sender}&Reciver=${ReciverInput.value}`;

    fetch(GetChatHistory).then(function (response) {
        return response.text();
    })
        .then(function (response) {
            navbar.innerHTML = response;
            AddEvent();
        })
        .catch(function () {
            console.log("something went wrong")
        });

    chatRoomValues = document.getElementById('chatroom');
    chatRoomValues.innerHTML = "";

     
};


