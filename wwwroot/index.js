const constructListComponent = async (root) => {
    let data;

    const resp = await fetch("./items");
    data = await resp.json();

    const renderItem = (item) => `
        <li>
            <input type="checkbox" name="items" id="${item.id}" value="${item.id}" />
            <label for="${item.id}">
                <div>${item.name}</div>
                <div>${item.description}</div>
            </label>
        </li>`;

    const render = () => {
        let html = data.map(d => renderItem(d)).join("");

        root.innerHTML = html;
    };

    render();
}

constructListComponent(document.querySelector("ul"));
