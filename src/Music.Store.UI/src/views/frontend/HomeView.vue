<template>
  <!-- ##### Hero Area Start ##### -->
  <section class="hero-area">
    <div class="hero-slides owl-carousel">
      <Carousel :wrapAround="true" :autoplay="5000">
        <Slide key="1">
          <!-- Single Hero Slide -->
          <div
            style="height: 700px !important"
            class="
              single-hero-slide
              d-flex
              align-items-center
              justify-content-center
            "
          >
            <!-- Slide Img -->
            <img class="slide-img bg-img" src="@/assets/img/bg-img/bg-1.jpg" />
            <!-- Slide Content -->
            <div class="container">
              <div class="row">
                <div class="col-12">
                  <div class="hero-slides-content text-center">
                    <h6 data-animation="fadeInUp" data-delay="100ms">
                      Latest album
                    </h6>
                    <h2 data-animation="fadeInUp" data-delay="300ms">
                      Beyond Time <span>Beyond Time</span>
                    </h2>
                    <a
                      data-animation="fadeInUp"
                      data-delay="500ms"
                      href="#"
                      class="btn oneMusic-btn mt-50"
                      >Discover <i class="fa fa-angle-double-right"></i
                    ></a>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </Slide>
        <Slide key="2">
          <!-- Single Hero Slide -->
          <div
            style="height: 700px !important"
            class="
              single-hero-slide
              d-flex
              align-items-center
              justify-content-center
            "
          >
            <!-- Slide Img -->
            <img class="slide-img bg-img" src="@/assets/img/bg-img/bg-2.jpg" />
            <!-- Slide Content -->
            <div class="container">
              <div class="row">
                <div class="col-12">
                  <div class="hero-slides-content text-center">
                    <h6 data-animation="fadeInUp" data-delay="100ms">
                      Latest album
                    </h6>
                    <h2 data-animation="fadeInUp" data-delay="300ms">
                      Colorlib Music <span>Colorlib Music</span>
                    </h2>
                    <a
                      data-animation="fadeInUp"
                      data-delay="500ms"
                      href="#"
                      class="btn oneMusic-btn mt-50"
                      >Discover <i class="fa fa-angle-double-right"></i
                    ></a>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </Slide>
        <template #addons>
          <Navigation />
          <Pagination />
        </template>
      </Carousel>
    </div>
  </section>
  <!-- ##### Hero Area End ##### -->

  <!-- ##### Latest Albums Area Start ##### -->
  <section class="latest-albums-area section-padding-100">
    <div class="container">
      <div class="row">
        <div class="col-12">
          <div class="section-heading style-2">
            <p>See what’s new</p>
            <h2>Latest Albums</h2>
          </div>
        </div>
      </div>
      <div class="row justify-content-center">
        <div class="col-12 col-lg-9">
          <div class="ablums-text text-center mb-70">
            <p>
              Nam tristique ex vel magna tincidunt, ut porta nisl finibus.
              Vivamus eu dolor eu quam varius rutrum. Fusce nec justo id sem
              aliquam fringilla nec non lacus. Suspendisse eget lobortis nisi,
              ac cursus odio. Vivamus nibh velit, rutrum at ipsum ac, dignissim
              iaculis ante. Donec in velit non elit pulvinar pellentesque et non
              eros.
            </p>
          </div>
        </div>
      </div>

      <div class="row">
        <div class="col-12">
          <div class="albums-slideshow owl-carousel">
            <Carousel
              :settings="settings"
              :breakpoints="breakpoints"
              :wrapAround="true"
              :autoplay="5000"
            >
              <Slide key="1">
                <!-- Single Album -->
                <div class="px-2">
                  <div class="single-album border">
                    <img src="@/assets/img/bg-img/a1.jpg" alt="" />
                    <div class="album-info">
                      <a href="#">
                        <h5>The Cure</h5>
                      </a>
                      <p>Second Song</p>
                    </div>
                  </div>
                </div>
              </Slide>
              <Slide key="2">
                <!-- Single Album -->
                <div class="px-2">
                  <div class="single-album border">
                    <img src="@/assets/img/bg-img/a2.jpg" alt="" />
                    <div class="album-info">
                      <a href="#">
                        <h5>Sam Smith</h5>
                      </a>
                      <p>Underground</p>
                    </div>
                  </div>
                </div>
              </Slide>
              <Slide key="3">
                <!-- Single Album -->
                <div class="px-2">
                  <div class="single-album border">
                    <img src="@/assets/img/bg-img/a3.jpg" alt="" />
                    <div class="album-info">
                      <a href="#">
                        <h5>Will I am</h5>
                      </a>
                      <p>First</p>
                    </div>
                  </div>
                </div>
              </Slide>
              <Slide key="4">
                <!-- Single Album -->
                <div class="px-2">
                  <div class="single-album border">
                    <img src="@/assets/img/bg-img/a4.jpg" alt="" />
                    <div class="album-info">
                      <a href="#">
                        <h5>The Cure</h5>
                      </a>
                      <p>Second Song</p>
                    </div>
                  </div>
                </div>
              </Slide>
              <Slide key="5">
                <!-- Single Album -->
                <div class="px-2">
                  <div class="single-album border">
                    <img src="@/assets/img/bg-img/a5.jpg" alt="" />
                    <div class="album-info">
                      <a href="#">
                        <h5>DJ SMITH</h5>
                      </a>
                      <p>The Album</p>
                    </div>
                  </div>
                </div>
              </Slide>
              <Slide key="6">
                <!-- Single Album -->
                <div class="px-2">
                  <div class="single-album border">
                    <img src="@/assets/img/bg-img/a6.jpg" alt="" />
                    <div class="album-info">
                      <a href="#">
                        <h5>The Ustopable</h5>
                      </a>
                      <p>Unplugged</p>
                    </div>
                  </div>
                </div>
              </Slide>
              <Slide key="7">
                <!-- Single Album -->
                <div class="px-2">
                  <div class="single-album border">
                    <img src="@/assets/img/bg-img/a7.jpg" alt="" />
                    <div class="album-info">
                      <a href="#">
                        <h5>Beyonce</h5>
                      </a>
                      <p>Songs</p>
                    </div>
                  </div>
                </div>
              </Slide>
              <template #addons>
                <Navigation />
                <Pagination />
              </template>
            </Carousel>
          </div>
        </div>
      </div>
    </div>
  </section>
  <!-- ##### Latest Albums Area End ##### -->

  <!-- ##### Buy Now Area Start ##### -->
  <section class="oneMusic-buy-now-area has-fluid bg-gray section-padding-100">
    <div class="container-fluid">
      <div class="row">
        <div class="col-12">
          <div class="section-heading style-2">
            <p>See what’s new</p>
            <h2>Buy What’s New</h2>
          </div>
        </div>
      </div>

      <div class="row">
        <!-- Single Album Area -->
        <div class="col-12 col-sm-6 col-md-4 col-lg-2">
          <div class="single-album-area wow fadeInUp" data-wow-delay="100ms">
            <div class="album-thumb">
              <img src="@/assets/img/bg-img/b1.jpg" alt="" />
              <!-- Album Price -->
              <div class="album-price">
                <p>$0.90</p>
              </div>
              <!-- Play Icon -->
              <div class="play-icon">
                <a href="#" class="video--play--btn"
                  ><span class="icon-play-button"></span
                ></a>
              </div>
            </div>
            <div class="album-info">
              <a href="#">
                <h5>Garage Band</h5>
              </a>
              <p>Radio Station</p>
            </div>
          </div>
        </div>

        <!-- Single Album Area -->
        <div class="col-12 col-sm-6 col-md-4 col-lg-2">
          <div class="single-album-area wow fadeInUp" data-wow-delay="200ms">
            <div class="album-thumb">
              <img src="@/assets/img/bg-img/b2.jpg" alt="" />
            </div>
            <div class="album-info">
              <a href="#">
                <h5>Noises</h5>
              </a>
              <p>Buble Gum</p>
            </div>
          </div>
        </div>

        <!-- Single Album Area -->
        <div class="col-12 col-sm-6 col-md-4 col-lg-2">
          <div class="single-album-area wow fadeInUp" data-wow-delay="300ms">
            <div class="album-thumb">
              <img src="@/assets/img/bg-img/b3.jpg" alt="" />
            </div>
            <div class="album-info">
              <a href="#">
                <h5>Jess Parker</h5>
              </a>
              <p>The Album</p>
            </div>
          </div>
        </div>

        <!-- Single Album Area -->
        <div class="col-12 col-sm-6 col-md-4 col-lg-2">
          <div class="single-album-area wow fadeInUp" data-wow-delay="400ms">
            <div class="album-thumb">
              <img src="@/assets/img/bg-img/b4.jpg" alt="" />
            </div>
            <div class="album-info">
              <a href="#">
                <h5>Noises</h5>
              </a>
              <p>Buble Gum</p>
            </div>
          </div>
        </div>

        <!-- Single Album Area -->
        <div class="col-12 col-sm-6 col-md-4 col-lg-2">
          <div class="single-album-area wow fadeInUp" data-wow-delay="500ms">
            <div class="album-thumb">
              <img src="@/assets/img/bg-img/b1.jpg" alt="" />
              <!-- Album Price -->
              <div class="album-price">
                <p>$0.90</p>
              </div>
              <!-- Play Icon -->
              <div class="play-icon">
                <a href="#" class="video--play--btn"
                  ><span class="icon-play-button"></span
                ></a>
              </div>
            </div>
            <div class="album-info">
              <a href="#">
                <h5>Garage Band</h5>
              </a>
              <p>Radio Station</p>
            </div>
          </div>
        </div>

        <!-- Single Album Area -->
        <div class="col-12 col-sm-6 col-md-4 col-lg-2">
          <div class="single-album-area wow fadeInUp" data-wow-delay="600ms">
            <div class="album-thumb">
              <img src="@/assets/img/bg-img/b2.jpg" alt="" />
            </div>
            <div class="album-info">
              <a href="#">
                <h5>Noises</h5>
              </a>
              <p>Buble Gum</p>
            </div>
          </div>
        </div>

        <!-- Single Album Area -->
        <div class="col-12 col-sm-6 col-md-4 col-lg-2">
          <div class="single-album-area wow fadeInUp" data-wow-delay="100ms">
            <div class="album-thumb">
              <img src="@/assets/img/bg-img/b3.jpg" alt="" />
            </div>
            <div class="album-info">
              <a href="#">
                <h5>Jess Parker</h5>
              </a>
              <p>The Album</p>
            </div>
          </div>
        </div>

        <!-- Single Album Area -->
        <div class="col-12 col-sm-6 col-md-4 col-lg-2">
          <div class="single-album-area wow fadeInUp" data-wow-delay="200ms">
            <div class="album-thumb">
              <img src="@/assets/img/bg-img/b4.jpg" alt="" />
            </div>
            <div class="album-info">
              <a href="#">
                <h5>Noises</h5>
              </a>
              <p>Buble Gum</p>
            </div>
          </div>
        </div>

        <!-- Single Album Area -->
        <div class="col-12 col-sm-6 col-md-4 col-lg-2">
          <div class="single-album-area wow fadeInUp" data-wow-delay="300ms">
            <div class="album-thumb">
              <img src="@/assets/img/bg-img/b1.jpg" alt="" />
              <!-- Album Price -->
              <div class="album-price">
                <p>$0.90</p>
              </div>
              <!-- Play Icon -->
              <div class="play-icon">
                <a href="#" class="video--play--btn"
                  ><span class="icon-play-button"></span
                ></a>
              </div>
            </div>
            <div class="album-info">
              <a href="#">
                <h5>Garage Band</h5>
              </a>
              <p>Radio Station</p>
            </div>
          </div>
        </div>

        <!-- Single Album Area -->
        <div class="col-12 col-sm-6 col-md-4 col-lg-2">
          <div class="single-album-area wow fadeInUp" data-wow-delay="400ms">
            <div class="album-thumb">
              <img src="@/assets/img/bg-img/b2.jpg" alt="" />
            </div>
            <div class="album-info">
              <a href="#">
                <h5>Noises</h5>
              </a>
              <p>Buble Gum</p>
            </div>
          </div>
        </div>

        <!-- Single Album Area -->
        <div class="col-12 col-sm-6 col-md-4 col-lg-2">
          <div class="single-album-area wow fadeInUp" data-wow-delay="500ms">
            <div class="album-thumb">
              <img src="@/assets/img/bg-img/b3.jpg" alt="" />
            </div>
            <div class="album-info">
              <a href="#">
                <h5>Jess Parker</h5>
              </a>
              <p>The Album</p>
            </div>
          </div>
        </div>

        <!-- Single Album Area -->
        <div class="col-12 col-sm-6 col-md-4 col-lg-2">
          <div class="single-album-area wow fadeInUp" data-wow-delay="600ms">
            <div class="album-thumb">
              <img src="@/assets/img/bg-img/b4.jpg" alt="" />
            </div>
            <div class="album-info">
              <a href="#">
                <h5>Noises</h5>
              </a>
              <p>Buble Gum</p>
            </div>
          </div>
        </div>
      </div>

      <div class="row">
        <div class="col-12">
          <div
            class="load-more-btn text-center wow fadeInUp"
            data-wow-delay="300ms"
          >
            <a href="#" class="btn oneMusic-btn"
              >Load More <i class="fa fa-angle-double-right"></i
            ></a>
          </div>
        </div>
      </div>
    </div>
  </section>
  <!-- ##### Buy Now Area End ##### -->

  <!-- ##### Miscellaneous Area Start ##### -->
  <section class="miscellaneous-area section-padding-100-0">
    <div class="container">
      <div class="row">
        <!-- ***** Weeks Top ***** -->
        <div class="col-12 col-lg-4">
          <div class="weeks-top-area mb-100">
            <div
              class="section-heading text-left mb-50 wow fadeInUp"
              data-wow-delay="50ms"
            >
              <p>See what’s new</p>
              <h2>This week’s top</h2>
            </div>

            <!-- Single Top Item -->
            <div
              class="single-top-item d-flex wow fadeInUp"
              data-wow-delay="100ms"
            >
              <div class="thumbnail">
                <img src="@/assets/img/bg-img/wt1.jpg" alt="" />
              </div>
              <div class="content-">
                <h6>Sam Smith</h6>
                <p>Underground</p>
              </div>
            </div>

            <!-- Single Top Item -->
            <div
              class="single-top-item d-flex wow fadeInUp"
              data-wow-delay="150ms"
            >
              <div class="thumbnail">
                <img src="@/assets/img/bg-img/wt2.jpg" alt="" />
              </div>
              <div class="content-">
                <h6>Power Play</h6>
                <p>In my mind</p>
              </div>
            </div>

            <!-- Single Top Item -->
            <div
              class="single-top-item d-flex wow fadeInUp"
              data-wow-delay="200ms"
            >
              <div class="thumbnail">
                <img src="@/assets/img/bg-img/wt3.jpg" alt="" />
              </div>
              <div class="content-">
                <h6>Cristinne Smith</h6>
                <p>My Music</p>
              </div>
            </div>

            <!-- Single Top Item -->
            <div
              class="single-top-item d-flex wow fadeInUp"
              data-wow-delay="250ms"
            >
              <div class="thumbnail">
                <img src="@/assets/img/bg-img/wt4.jpg" alt="" />
              </div>
              <div class="content-">
                <h6>The Music Band</h6>
                <p>Underground</p>
              </div>
            </div>

            <!-- Single Top Item -->
            <div
              class="single-top-item d-flex wow fadeInUp"
              data-wow-delay="300ms"
            >
              <div class="thumbnail">
                <img src="@/assets/img/bg-img/wt5.jpg" alt="" />
              </div>
              <div class="content-">
                <h6>Creative Lyrics</h6>
                <p>Songs and stuff</p>
              </div>
            </div>

            <!-- Single Top Item -->
            <div
              class="single-top-item d-flex wow fadeInUp"
              data-wow-delay="350ms"
            >
              <div class="thumbnail">
                <img src="@/assets/img/bg-img/wt6.jpg" alt="" />
              </div>
              <div class="content-">
                <h6>The Culture</h6>
                <p>Pop Songs</p>
              </div>
            </div>
          </div>
        </div>

        <!-- ***** New Hits Songs ***** -->
        <div class="col-12 col-lg-4">
          <div class="new-hits-area mb-100">
            <div
              class="section-heading text-left mb-50 wow fadeInUp"
              data-wow-delay="50ms"
            >
              <p>See what’s new</p>
              <h2>New Hits</h2>
            </div>

            <!-- Single Top Item -->
            <div
              class="
                single-new-item
                d-flex
                align-items-center
                justify-content-between
                wow
                fadeInUp
              "
              data-wow-delay="100ms"
            >
              <div class="first-part d-flex align-items-center">
                <div class="thumbnail">
                  <img src="@/assets/img/bg-img/wt7.jpg" alt="" />
                </div>
                <div class="content-">
                  <h6>Sam Smith</h6>
                  <p>Underground</p>
                </div>
              </div>
              <audio preload="auto" controls>
                <source src="@/assets/audio/dummy-audio.mp3" />
              </audio>
            </div>

            <!-- Single Top Item -->
            <div
              class="
                single-new-item
                d-flex
                align-items-center
                justify-content-between
                wow
                fadeInUp
              "
              data-wow-delay="150ms"
            >
              <div class="first-part d-flex align-items-center">
                <div class="thumbnail">
                  <img src="@/assets/img/bg-img/wt8.jpg" alt="" />
                </div>
                <div class="content-">
                  <h6>Power Play</h6>
                  <p>In my mind</p>
                </div>
              </div>
              <audio preload="auto" controls>
                <source src="@/assets/audio/dummy-audio.mp3" />
              </audio>
            </div>

            <!-- Single Top Item -->
            <div
              class="
                single-new-item
                d-flex
                align-items-center
                justify-content-between
                wow
                fadeInUp
              "
              data-wow-delay="200ms"
            >
              <div class="first-part d-flex align-items-center">
                <div class="thumbnail">
                  <img src="@/assets/img/bg-img/wt9.jpg" alt="" />
                </div>
                <div class="content-">
                  <h6>Cristinne Smith</h6>
                  <p>My Music</p>
                </div>
              </div>
              <audio preload="auto" controls>
                <source src="@/assets/audio/dummy-audio.mp3" />
              </audio>
            </div>

            <!-- Single Top Item -->
            <div
              class="
                single-new-item
                d-flex
                align-items-center
                justify-content-between
                wow
                fadeInUp
              "
              data-wow-delay="250ms"
            >
              <div class="first-part d-flex align-items-center">
                <div class="thumbnail">
                  <img src="@/assets/img/bg-img/wt10.jpg" alt="" />
                </div>
                <div class="content-">
                  <h6>The Music Band</h6>
                  <p>Underground</p>
                </div>
              </div>
              <audio preload="auto" controls>
                <source src="@/assets/audio/dummy-audio.mp3" />
              </audio>
            </div>

            <!-- Single Top Item -->
            <div
              class="
                single-new-item
                d-flex
                align-items-center
                justify-content-between
                wow
                fadeInUp
              "
              data-wow-delay="300ms"
            >
              <div class="first-part d-flex align-items-center">
                <div class="thumbnail">
                  <img src="@/assets/img/bg-img/wt11.jpg" alt="" />
                </div>
                <div class="content-">
                  <h6>Creative Lyrics</h6>
                  <p>Songs and stuff</p>
                </div>
              </div>
              <audio preload="auto" controls>
                <source src="@/assets/audio/dummy-audio.mp3" />
              </audio>
            </div>

            <!-- Single Top Item -->
            <div
              class="
                single-new-item
                d-flex
                align-items-center
                justify-content-between
                wow
                fadeInUp
              "
              data-wow-delay="350ms"
            >
              <div class="first-part d-flex align-items-center">
                <div class="thumbnail">
                  <img src="@/assets/img/bg-img/wt12.jpg" alt="" />
                </div>
                <div class="content-">
                  <h6>The Culture</h6>
                  <p>Pop Songs</p>
                </div>
              </div>
              <audio preload="auto" controls>
                <source src="@/assets/audio/dummy-audio.mp3" />
              </audio>
            </div>
          </div>
        </div>

        <!-- ***** Popular Artists ***** -->
        <div class="col-12 col-lg-4">
          <div class="popular-artists-area mb-100">
            <div
              class="section-heading text-left mb-50 wow fadeInUp"
              data-wow-delay="50ms"
            >
              <p>See what’s new</p>
              <h2>Popular Artist</h2>
            </div>

            <!-- Single Artist -->
            <div
              class="single-artists d-flex align-items-center wow fadeInUp"
              data-wow-delay="100ms"
            >
              <div class="thumbnail">
                <img src="@/assets/img/bg-img/pa1.jpg" alt="" />
              </div>
              <div class="content-">
                <p>Sam Smith</p>
              </div>
            </div>

            <!-- Single Artist -->
            <div
              class="single-artists d-flex align-items-center wow fadeInUp"
              data-wow-delay="150ms"
            >
              <div class="thumbnail">
                <img src="@/assets/img/bg-img/pa2.jpg" alt="" />
              </div>
              <div class="content-">
                <p>William Parker</p>
              </div>
            </div>

            <!-- Single Artist -->
            <div
              class="single-artists d-flex align-items-center wow fadeInUp"
              data-wow-delay="200ms"
            >
              <div class="thumbnail">
                <img src="@/assets/img/bg-img/pa3.jpg" alt="" />
              </div>
              <div class="content-">
                <p>Jessica Walsh</p>
              </div>
            </div>

            <!-- Single Artist -->
            <div
              class="single-artists d-flex align-items-center wow fadeInUp"
              data-wow-delay="250ms"
            >
              <div class="thumbnail">
                <img src="@/assets/img/bg-img/pa4.jpg" alt="" />
              </div>
              <div class="content-">
                <p>Tha Stoves</p>
              </div>
            </div>

            <!-- Single Artist -->
            <div
              class="single-artists d-flex align-items-center wow fadeInUp"
              data-wow-delay="300ms"
            >
              <div class="thumbnail">
                <img src="@/assets/img/bg-img/pa5.jpg" alt="" />
              </div>
              <div class="content-">
                <p>DJ Ajay</p>
              </div>
            </div>

            <!-- Single Artist -->
            <div
              class="single-artists d-flex align-items-center wow fadeInUp"
              data-wow-delay="350ms"
            >
              <div class="thumbnail">
                <img src="@/assets/img/bg-img/pa6.jpg" alt="" />
              </div>
              <div class="content-">
                <p>Radio Vibez</p>
              </div>
            </div>

            <!-- Single Artist -->
            <div
              class="single-artists d-flex align-items-center wow fadeInUp"
              data-wow-delay="400ms"
            >
              <div class="thumbnail">
                <img src="@/assets/img/bg-img/pa7.jpg" alt="" />
              </div>
              <div class="content-">
                <p>Music 4u</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
  <!-- ##### Miscellaneous Area End ##### -->

  <!-- ##### Newsletter & Testimonials Area Start ##### -->
  <section class="newsletter-testimonials-area">
    <div class="container">
      <div class="row">
        <!-- Newsletter Area -->
        <div class="col-12 col-lg-6">
          <div class="newsletter-area mb-100">
            <div class="section-heading text-left mb-50">
              <p>See what’s new</p>
              <h2>Subscribe to newsletter</h2>
            </div>
            <div class="newsletter-form">
              <form action="#">
                <el-input placeholder="E-mail address" v-model="emailAddress" class="mb-3"/>

                <button type="submit" class="btn oneMusic-btn float-end">
                  Subscribe <i class="fa fa-angle-double-right"></i>
                </button>
              </form>
            </div>
          </div>
        </div>

        <!-- Testimonials Area -->
        <div class="col-12 col-lg-6">
          <div
            class="testimonials-area mb-100 bg-img bg-overlay"
            style="background-image: url(img/bg-img/bg-3.jpg)"
          >
            <div class="section-heading white text-left mb-50">
              <p>See what’s new</p>
              <h2>Testimonial</h2>
            </div>
            <!-- Testimonial Slide -->
            <div class="testimonials-slide owl-carousel">
              <!-- Single Slide -->
              <div class="single-slide">
                <p>
                  Nam tristique ex vel magna tincidunt, ut porta nisl finibus.
                  Vivamus eu dolor eu quam varius rutrum. Fusce nec justo id sem
                  aliquam fringilla nec non lacus. Suspendisse eget lobortis
                  nisi, ac cursus odio. Vivamus nibh velit, rutrum.
                </p>
                <div class="testimonial-info d-flex align-items-center">
                  <div class="testimonial-thumb">
                    <img src="@/assets/img/bg-img/t1.jpg" alt="" />
                  </div>
                  <p>William Smith, Customer</p>
                </div>
              </div>
              <!-- Single Slide -->
              <div class="single-slide">
                <p>
                  Nam tristique ex vel magna tincidunt, ut porta nisl finibus.
                  Vivamus eu dolor eu quam varius rutrum. Fusce nec justo id sem
                  aliquam fringilla nec non lacus. Suspendisse eget lobortis
                  nisi, ac cursus odio. Vivamus nibh velit, rutrum.
                </p>
                <div class="testimonial-info d-flex align-items-center">
                  <div class="testimonial-thumb">
                    <img src="@/assets/img/bg-img/t1.jpg" alt="" />
                  </div>
                  <p>Nazrul Islam, Developer</p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
  <!-- ##### Newsletter & Testimonials Area End ##### -->
</template>

<script lang="ts" setup>
import { ref } from "@vue/reactivity";
import { Carousel, Slide, Pagination, Navigation } from "vue3-carousel";

let emailAddress = ref("");

const settings = {
  itemsToShow: 1,
  snapAlign: "center",
};
const breakpoints = {
  700: {
    itemsToShow: 3.5,
    snapAlign: "center",
  },
  1024: {
    itemsToShow: 5,
    snapAlign: "start",
  },
};
</script>