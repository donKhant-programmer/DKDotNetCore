const tblBlog = "Blogs"
let blogId = null;

getBlogTable();
// createBlog();
// editBlog("15d42823-1c63-4dbd-a3e9-d43161afba03", "ed ited", "edited", "edited");
// deleteBlog("15d42823-1c63-4dbd-a3e9-d43161afba03");

function readBlog() {
    const blogs = getBlogs();
    console.log(blogs);
}

function createBlog(title, author, content) {
    let lst = getBlogs();

    const requestModel = {
        id: uuidv4(),
        title: title,
        author: author,
        content: content
    }

    lst.push(requestModel);
    const jsonStr = JSON.stringify(lst);
    localStorage.setItem(tblBlog, jsonStr);

    successMessage("Saving successful");
    clearControls();
}

function editBlog(id) {
    let lst = getBlogs();

    const items = lst.filter(x => x.id == id);
    if (items.length == 0) {
        console.log("No data found!");
        errorMessage("No data found!");
        return;
    }

    blogId = id;

    let item = items[0];
    $('#txtTitle').val(item.title);
    $('#txtAuthor').val(item.author);
    $('#txtContent').val(item.content);

    $('#txtTitle').focus();
}

function updateBlog(id, title, author, content) {
    let lst = getBlogs();

    const items = lst.filter(x => x.id == id);
    if (items.length == 0) {
        console.log("No data found!");
        errorMessage("No data found!");
        return;
    }

    const item = items[0];
    item.title = title;
    item.author = author;
    item.content = content;

    const index = lst.findIndex(x => x.id === id);
    lst[index] = item;

    const jsonStr = JSON.stringify(lst);
    localStorage.setItem(tblBlog, jsonStr);

    successMessage('Updating successful!');
    clearControls();
}

function deleteBlog(id) {
    let result = confirm('Are you sure want to delete this?');
    if (!result) return;

    let lst = getBlogs();

    const items = lst.filter(x => x.id == id);
    if (items.length == 0) {
        console.log("No data found!");
        return;
    }

    lst = lst.filter(x => x.id !== id);

    const jsonStr = JSON.stringify(lst);
    localStorage.setItem(tblBlog, jsonStr);

    successMessage('Deleting successful!');
    getBlogTable();
}

function getBlogs() {
    const blogs = localStorage.getItem(tblBlog);
    console.log(blogs);

    let lst = [];
    if (blogs != null) {
        lst = JSON.parse(blogs);
    }

    return lst;
}

function getBlogTable() {
    const lst = getBlogs();
    let count = 0;
    let htmlRows = '';
    lst.forEach(item => {
        const htmlRow = `
        <tr>
            <td>
                <button type="button" class="btn btn-warning" onClick="editBlog('${item.id}')">Edit</button>
                <button type="button" class="btn btn-danger" onClick="deleteBlog('${item.id}')">Delete</button>
            </td>
            <td>${++count}</td>
            <td>${item.title}</td>
            <td>${item.author}</td>
            <td>${item.content}</td>
        </tr>
        `;
    htmlRows += htmlRow;
    });

    $('#tbody').html(htmlRows);
}

function uuidv4() {
    return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, c =>
        (+c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> +c / 4).toString(16)
    );
}

$('#btnSave').click(function() {
    const title = $('#txtTitle').val();
    const author = $('#txtAuthor').val();
    const content = $('#txtContent').val();

    if (blogId === null) {
        createBlog(title, author, content);
    } else {
        updateBlog(blogId, title, author, content);
        blogId = null;
    }

    getBlogTable();
})

$('#btnCancel').click(function() {
    clearControls();
    blogId = null;
})

function successMessage(message) {
    alert(message);
}

function errorMessage(message) {
    alert(message);
}

function clearControls() {
    $('#txtTitle').val('');
    $('#txtAuthor').val('');
    $('#txtContent').val('');
    $('#txtTitle').focus();
}
