let token;
const connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:5189/chat/", { accessTokenFactory: () => token }).build();

$(document).ready(function () {
    $("#loginButton").on("click", function () {
        const loginData = {
            email: $("#userName").val(),
            password: $("#userPassword").val(),
        };

        $.ajax({
            url: "http://localhost:5131/api/login/login",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(loginData),
            success: function (data) {
                token = data.access_token;
                console.log(token);
                $("#sendButton").prop("disabled", false);

                connection.start()
                    .catch(err => {
                        console.error(err.toString());
                        $("#loginButton, #sendButton").prop("disabled", true);
                    });
            }
        });
    });

    connection.on("Receive chat history", function (chatHistory) {
        const list = JSON.parse(chatHistory);
        const ul = $("#chatBox");

        for (let i = 0; i < list.length; i += 1) {
            const dateTime = list[i]["DateTime"];
            const time = $("<time>").attr("datetime", dateTime).text(new Date(dateTime).toLocaleString());
            const newItem = $("<li>");
            const paragraph = $("<p>").html("<strong>" + list[i]["SenderId"] + "</strong>: " + list[i]["Text"]);

            newItem.append(paragraph, time);
            ul.append(newItem);
        }
    });

    function clearBox() {
        $("#chatBox").empty();
    }

    $("#getChatHistoryButton").on("click", function () {
        clearBox();
        const otherUserId = $("#otherUserId").val();
        const pageNumber = parseInt($("#pageNum").val(), 10);
        const pageSize = parseInt($("#pageSize").val(), 10);
        const controller = new AbortController();

        connection.invoke("GetChatHistoryAsync", otherUserId, pageNumber, pageSize, controller.signal)
            .catch(err => {
                console.error(err.toString());
            })
            .finally(() => {
                controller.abort();
            });
    });

    $("#sendButton").on("click", function () {
        const consumerId = $("#consumerId").val();
        const message = $("#message").val();
        const controller = new AbortController();
        connection.invoke("SendAsync", consumerId, message, controller.signal)
            .catch(err => {
                console.error(err.toString());
            })
            .finally(() => {
                controller.abort();
            });
    });
});
