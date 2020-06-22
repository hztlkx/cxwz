//ajax同步通信
function GetAjaxComplete(value, url) {
    var returnArray = [],
        dataArray,
        innerObject = {},
        postParamsObj = { "key": value };

    $.ajax({
        type: "POST",
        url: url,
        data: postParamsObj,
        async: false,
        success: function (msg) {
            dataArray = msg['Data'];
            for (var i = 0; i < dataArray.length; i++) {
                innerObject = {};
                innerObject["rowNumber"] = parseInt(i + 1) + "、";
                innerObject["id"] = dataArray[i].id;
                innerObject["text"] = dataArray[i].text;
                innerObject["highlight"] = dataArray[i].highlight;
                returnArray.push(innerObject);
            }
        },
        error: function (msg) {

        }
    });
    return returnArray;
}

//el:element标签
//url:数据访问地址
//t:组件类型
//w:组件宽度
function AutoComplete(el, url, t, w) {
    var _w = "50%";//默认宽度
    if (w) {
        _w = w;
    }
    //多选
    if (t) {
        var multipleHtml = '<div class="ac-input">';
        multipleHtml += '<input id="' + el + 'Input" name="' + el + 'Input" placeholder="请输入搜索关键字..."' +
                        ' data-activates="' + el + 'Dropdown" data-beloworigin="true" ' +
                        'autocomplete="off" style="max-width:' + _w +' !important;">' +
                        '</input>';
        multipleHtml += '<ul id="' + el + 'Dropdown" class="dropdown-content ac-dropdown"></ul>';
        multipleHtml += "<br/><input id='" + el + "Hidden'  name='" + el +"Hidden'  type='hidden'>";
        $("#" + el).html(multipleHtml + "</div>");

        if (t === "multiple") {
            var multiple = $('#multipleInput').materialize_autocomplete({
                cacheable: false,
                multiple: {
                    enable: true,
                    maxSize: 100,
                    onExist: function (item) {
                        Materialize.toast('Tag: ' + item.text + '(' + item.id + ') 已经添加!', 2000);
                    },
                    onAppend: function (item) {
                        var itemIds = $("#" + el + "Hidden").val();
                        if (itemIds === "") {
                            $("#" + el + "Hidden").val(item.id);
                        } else {
                            $("#" + el + "Hidden").val(itemIds + "," + item.id);
                        }
                    },
                    onRemove: function (item) {
                        var itemIds = $("#" + el + "Hidden").val();
                        var arr = itemIds.split(",");
                        for (var i in arr) {
                            if (arr[i] === item.id.toString()) {
                                arr.splice(i, 1);
                                break; //该行代码变成i--,则移除所有2
                            }
                        }
                        $("#" + el + "Hidden").val(arr.toString());
                    }
                },
                dropdown: {
                    el: '#multipleDropdown',
                    itemTemplate: '<li class="ac-item" data-id="<%= item.id %>" data-text=\'<%= item.text %>\'>' +
                        '<a href="javascript:void(0)"><%= item.rowNumber %><%= item.highlight %></a></li>',
                    noItem: "<li class='ac-item'><a href='javascript:void(0)' title=" + el + " onclick='SelectItem(this)'>没有搜索到结果</a></li>"
                },
                onSelect: function (item) {
                    $("#" + el + "Hidden").val(item.id);
                    //console.log(item.text + ' was selected');
                },
                appender: {
                    el: '',
                    tagName: 'ul',
                    className: 'ac-appender',
                    tagTemplate: '<div class="chip" data-id="<%= item.id %>" data-text="<%= item.text %>" style="background-color:#34A853;color:white;">' +
                        '<%= item.text %>(<%= item.id %>)' +
                        '<span class="close" style="opacity:1;line-height:2;color:orange;cursor:pointer;padding-left:4px;font-size:12pt;">X</span></div>'
                },
                getData: function (value, callback) {
                    var rst = GetAjaxComplete(value, url);
                    this.limit = rst.length;
                    callback(value, rst);
                }
            });
        }
    }
    //单选
    else {
        var singleHtml = '<div class="ac-input">';
        singleHtml += '<input id="' + el + 'Input" name="' + el + 'Input" placeholder="请输入搜索关键字..."' +
                      ' data-activates="' + el + 'Dropdown" data-beloworigin="true" ' +
            'autocomplete="off" style="max-width:' + _w + ' !important;">' +
                      '</input>';
        singleHtml += '<ul id="' + el + 'Dropdown" class="dropdown-content ac-dropdown"></ul>';
        singleHtml += "<br/><input id='" + el + "Hidden' name='" + el + "Hidden' type='hidden'>";
        $("#" + el).html(singleHtml + "</div>");

        $("#"+el+"Input").materialize_autocomplete({
            cacheable: false,
            multiple: {
                enable: false
            },
            dropdown: {
                el: "#"+el+"Dropdown",
                itemTemplate: '<li class="ac-item" data-id="<%= item.id %>" data-text=\'<%= item.text %>\'>' +
                    '<a href="javascript:void(0)"><%= item.rowNumber %><%= item.highlight %></a></li>',
                noItem: "<li class='ac-item'><a href='javascript:void(0)' title="+el+" onclick='SelectItem(this)'>没有搜索到结果</a></li>"
            },
            onSelect: function (item) {
                $("#" + el + "Hidden").val(item.id);
                //console.log(item.text + ' was selected');
            },
            getData: function (value, callback) {
                var rst = GetAjaxComplete(value,url);
                this.limit = rst.length;
                callback(value, rst);
            }
        });
    }
}

//选中事件[没有搜索到结果]
function SelectItem(el) {
    $("#" + el.title + "Input").val("");
}
