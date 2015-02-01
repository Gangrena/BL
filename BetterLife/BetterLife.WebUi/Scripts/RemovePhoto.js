function removeNestedForm(element, container, deleteElement) {
    var $container = $(element).parent(container);
    $container.find(deleteElement).val('True');
    $container.fadeOut(1000);
};