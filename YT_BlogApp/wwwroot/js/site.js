const navItems = document.querySelector('nav_items');
const open_NavBtn = document.querySelector('open_nav-btn');
const closeNavBtn = document.querySelector('close_nav-btn');

///open nav bar button ///
const open_nav_func = () => {
    navItems.style.display = "opacity"
    open_NavBtn.style.display = 1;
    closeNavBtn.style.display = 0;
}

///close nav bar button ///
const close_nav_func = () => {
    navItems.style.display = "none"
    open_NavBtn.style.display = 'inline-block';
    closeNavBtn.style.display = 'none';
}


open_NavBtn.addEventListener('click', open_nav_func);
closeNavBtn.addEventListener('click', close_nav_func)


////////////////////
document.getElementById('AddUserbtn').addEventListener('click', function () {
    // Get the values of the password fields
    var password = document.getElementById('createPassword').value;
    var confirmPassword = document.getElementById('confirmPassword').value;

    // Get the message display element
    /*var messageElement = document.getElementById('passwordMessage');*/

    // Check if the passwords match
    if (password === confirmPassword) {
        console.log("Match");
        //messageElement.textContent = "Passwords match!";
        //messageElement.style.color = "green"; // Change text color to green
    } else {
        console.log("Doesn't Match");
        //messageElement.textContent = "Passwords do not match!";
        //messageElement.style.color = "red"; // Change text color to red
    }
});