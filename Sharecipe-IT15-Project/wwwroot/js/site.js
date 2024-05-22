// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener('DOMContentLoaded', function () {
    var deleteConfirmationModal = document.getElementById('deleteConfirmationModal');
    deleteConfirmationModal.addEventListener('show.bs.modal', function (event) {
        var button = event.relatedTarget; // Button that triggered the modal
        var postId = button.getAttribute('data-post-id'); // Extract postId from data-* attributes

        // Update the modal's content
        var modalPostIdInput = document.getElementById('modalPostId');
        modalPostIdInput.value = postId;
    });
});

function hideEditPostModal() {
    var modal = new bootstrap.Modal(document.getElementById('editPostModal'));
    modal.hide();
}
