const togglePassword = document.querySelector('#togglePassword');
const password = document.querySelector('#password');

togglePassword.addEventListener('click', function (e) {
    const type = password.getAttribute('type') === 'password' ? 'text' : 'password';
    password.setAttribute('type', type);
    if (type === 'password') {
        this.innerHTML = '<i class="fas fa-eye-slash"></i>';
    } else {
        this.innerHTML = '<i class="fas fa-eye"></i>';
    }
});