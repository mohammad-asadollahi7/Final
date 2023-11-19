
function GetAttributeTitles() {
    var container_div = document.getElementById("attributeDiv");
    container_div.innerHTML = "";


    var categoryId = document.getElementById("select-category").value;

    $.ajax({

        url: `http://localhost:5153/api/Category/GetCustomAttributeTitles/` + categoryId,
        type: 'get',
        dataType: 'json',
        success: function (response) {

            for (var i = 0; i < response.length; i++) {

                const input1 = document.createElement("input");
                input1.classList.add('form-control', 'mt-1');
                input1.setAttribute('value', response[i].title);
                input1.setAttribute('name', `CustomAttributes[${i}].title`);
                input1.setAttribute('type', 'hidden');

                const input2 = document.createElement("input");
                input2.classList.add('form-control', 'mt-1');
                input2.setAttribute('name', `CustomAttributes[${i}].id`);
                input2.setAttribute('value', response[i].id);
                input2.setAttribute('type', 'hidden');

                const label = document.createElement("label");
                label.classList.add('form-label', 'mt-2');
                label.innerHTML = response[i].title;

                const input = document.createElement("input");
                input.classList.add('form-control', 'mt-1');
                input.setAttribute('name', `CustomAttributes[${i}].value`);
                input.setAttribute('required', '');
                input.setAttribute('oninvalid', 'this.setCustomValidity("فیلد الزامی")');
                input.setAttribute('oninput', 'this.setCustomValidity("")');


                var div = document.getElementById('attributeDiv');
                div.append(label);
                div.append(input1);
                div.append(input2);
                div.append(input);
            }

        },
        error: function (error) {

            alert(JSON.stringify(error.responseText));
        }
    })
}
