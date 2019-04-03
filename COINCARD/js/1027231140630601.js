window.hrfwidget = {
    appId: '363772567412181',
    pageId: '1027231140630601',
    widgets: [{"id":"51750","type":"customer_chat","ref":"__hrf_w_51750"}],
    checkboxs:[],
    customer_chats:[{"_id":5320,"growthtool_id":51750,"logged_in_greeting":null,"logged_out_greeting":null,"greeting_dialog_display":"show"}]
};

(function(d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s);
    js.id = id;
    js.src = '//harafunnel.com/widget.js?' + (Math.round(+new Date / 1000 * 600));
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'hrfwidget-core'));

function confirmOptIn(id) {
        var w = hrfwidget.widgets.filter(a => a.id == id)

        if (w && w.length > 0) {
            var x = document.getElementById(id);
            var userRef = x.getAttribute('user_ref');
            FB.AppEvents.logEvent('MessengerCheckboxUserConfirmation', null, {
                'app_id': '363772567412181',
                'page_id': '1027231140630601',
                'ref': w[0].ref,
                'user_ref': userRef
            });
        }
    }