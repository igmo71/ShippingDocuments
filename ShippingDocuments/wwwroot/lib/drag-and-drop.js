document.addEventListener('DOMContentLoaded', function () {
    const items = document.querySelectorAll('.card');
    const columns = document.querySelectorAll('.kanban-column');

    items.forEach(item => {
        item.draggable = true;

        item.addEventListener('dragstart', function () {
            this.classList.add('dragging');
        });

        item.addEventListener('dragend', function () {
            this.classList.remove('dragging');
        });
    });

    columns.forEach(column => {
        column.addEventListener('dragover', function (e) {
            e.preventDefault();
            const draggingItem = document.querySelector('.dragging');
            this.querySelector('.kanban-items').appendChild(draggingItem);
        });
    });
});