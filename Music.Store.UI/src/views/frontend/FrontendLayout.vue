<template>
  <!-- ##### Header Area Start ##### -->
  <header class="header-area">
    <!-- Navbar Area -->
    <div id="sticky-wrapper" class="sticky-wrapper" style="height: 85px">
      <div class="oneMusic-main-menu">
        <div class="classy-nav-container breakpoint-off light left">
          <div class="container">
            <!-- Menu -->
            <nav class="classy-navbar justify-content-between" id="oneMusicNav">
              <!-- Nav brand -->
              <router-link to="/" class="nav-brand"
                ><img src="@/assets/img/core-img/logo.png" alt=""
              /></router-link>

              <!-- Navbar Toggler -->
              <div class="classy-navbar-toggler">
                <span class="navbarToggler"
                  ><span></span><span></span><span></span
                ></span>
              </div>

              <!-- Menu -->
              <div class="classy-menu">
                <!-- Close Button -->
                <div class="classycloseIcon">
                  <div class="cross-wrap">
                    <span class="top"></span><span class="bottom"></span>
                  </div>
                </div>

                <!-- Nav Start -->
                <div class="classynav">
                  <ul>
                    <li><router-link to="/">Home</router-link></li>
                    <li><router-link to="/album">Albums</router-link></li>
                    <li class="cn-dropdown-item has-down">
                      <a href="#">Pages</a>
                      <ul class="dropdown">
                        <li><a href="index.html">Home</a></li>
                        <li><a href="albums-store.html">Albums</a></li>
                        <li><a href="event.html">Events</a></li>
                        <li><a href="blog.html">News</a></li>
                        <li><a href="contact.html">Contact</a></li>
                        <li><a href="elements.html">Elements</a></li>
                        <li><a href="login.html">Login</a></li>
                        <li class="has-down">
                          <a href="#">Dropdown</a>
                          <ul class="dropdown">
                            <li><a href="#">Even Dropdown</a></li>
                            <li><a href="#">Even Dropdown</a></li>
                            <li><a href="#">Even Dropdown</a></li>
                            <li class="has-down">
                              <a href="#">Even Dropdown</a>
                              <ul class="dropdown">
                                <li><a href="#">Deeply Dropdown</a></li>
                                <li><a href="#">Deeply Dropdown</a></li>
                                <li><a href="#">Deeply Dropdown</a></li>
                                <li><a href="#">Deeply Dropdown</a></li>
                                <li><a href="#">Deeply Dropdown</a></li>
                              </ul>
                              <span class="dd-trigger"></span>
                            </li>
                            <li><a href="#">Even Dropdown</a></li>
                          </ul>
                          <span class="dd-trigger"></span>
                        </li>
                      </ul>
                      <span class="dd-trigger"></span>
                    </li>
                    <li><router-link to="event">Events</router-link></li>
                    <li><router-link to="news">News</router-link></li>
                    <li><router-link to="contact">Contact</router-link></li>
                  </ul>

                  <!-- Login/Register & Cart Button -->
                  <div
                    class="login-register-cart-button d-flex align-items-center"
                  >
                    <!-- Login/Register -->
                    <div class="login-register-btn">
                      <router-link to="/login" id="loginBtn"
                        >Login / Register</router-link
                      >
                    </div>
                  </div>
                </div>
                <!-- Nav End -->
              </div>
            </nav>
          </div>
        </div>
      </div>
    </div>
  </header>
  <!-- ##### Header Area End ##### -->

  <div class="my-3">
    <div class="container">
      <div class="row">
        <div class="col-md-12">
          <router-view></router-view>
        </div>
      </div>
    </div>
  </div>

  <!-- ##### Footer Area Start ##### -->
  <footer class="footer-area">
    <div class="container">
      <div class="row d-flex flex-wrap align-items-center">
        <div class="col-12 col-md-6">
          <router-link to="/">
            <img src="@/assets/img/core-img/logo.png" alt=""
          /></router-link>
          <p class="copywrite-text">
            <a href="#">
              Copyright &copy; All rights reserved | This template is made with
              <i class="fa fa-heart-o" aria-hidden="true"></i> by
            </a>
          </p>
        </div>

        <div class="col-12 col-md-6">
          <div class="footer-nav">
            <ul>
              <li><router-link to="/">Home</router-link></li>
              <li><router-link to="/album">Albums</router-link></li>
              <li><router-link to="/event">Events</router-link></li>
              <li><router-link to="/news">News</router-link></li>
              <li><router-link to="/contact">Contact</router-link></li>
            </ul>
          </div>
        </div>
      </div>
    </div>
  </footer>
  <!-- ##### Footer Area Start ##### -->
</template>

<script lang="ts" setup>
</script>