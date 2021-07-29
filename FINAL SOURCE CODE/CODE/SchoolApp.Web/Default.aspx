<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SchoolApp.Web.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CSSContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <!--Konnect Slider -->
<div class='konnect-carousel carousel-image carousel-image-pagination carousel-image-arrows flexslider'>
  <ul class='slides'>
    <!--Slider One-->
    <li class='item'>
      <div class='container'>
        <div class='row pos-rel'>
          <div class='col-sm-12 col-md-6 animate'>
            <h1 class='big fadeInDownBig animated'>Slider Heading 1</h1>
            <p class='normal fadeInUpBig animated delay-point-five-s'>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris in tincidunt mauris. Etiam arcu enim, laoreet vitae orci vel, rutrum feugiat nibh. Integer feugiat ligula tellus, non pulvinar justo pharetra eu. Nullam vehicula lorem ut diam tincidunt sagittis. Morbi est ligula, posuere in laoreet ac, porta porttitor dui</p>
            <a class='btn btn-bordered btn-white btn-lg fadeInRightBig animated delay-one-s' href='#'> Show more </a> </div>
          <div class='col-md-6 animate pos-sta hidden-xs hidden-sm'> <img class="img-responsive img-right fadeInUpBig animated delay-one-point-five-s" alt="iPhone" src="img/slider/student-1.png" /> </div>
        </div>
      </div>
    </li>
    
    <!--Slider Two-->
    <li class='item'>
      <div class='container'>
        <div class='row pos-rel'>
          <div class='col-md-6 animate pos-sta hidden-xs hidden-sm'> <img class="img-responsive img-left fadeInUpBig animated" alt="Circle" src="img/slider/student-2.png" /> </div>
          <div class='col-sm-12 col-md-6 animate'>
            <h2 class='big fadeInUpBig animated delay-point-five-s'>Slider Heading 2</h2>
            <p class='normal fadeInDownBig animated delay-one-s'>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris in tincidunt mauris. Etiam arcu enim, laoreet vitae orci vel, rutrum feugiat nibh. Integer feugiat ligula tellus, non pulvinar justo pharetra eu. Nullam vehicula lorem ut diam tincidunt sagittis. Morbi est ligula, posuere in laoreet ac, porta porttitor dui</p>
            <a class='btn btn-bordered btn-white btn-lg fadeInLeftBig animated delay-one-point-five-s' href='#'> Show more </a> </div>
        </div>
      </div>
    </li>
    
    <!--Slider Three-->
    <li class='item'>
      <div class='container'>
        <div class='row pos-rel'>
          <div class='col-sm-12 col-md-6 animate'>
            <h2 class='big fadeInLeftBig animated'>Slider Heading 3</h2>
            <p class='normal fadeInRightBig animated delay-point-five-s'>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris in tincidunt mauris. Etiam arcu enim, laoreet vitae orci vel, rutrum feugiat nibh. Integer feugiat ligula tellus, non pulvinar justo pharetra eu. Nullam vehicula lorem ut diam tincidunt sagittis. Morbi est ligula, posuere in laoreet ac, porta porttitor dui</p>
            <a class='btn btn-bordered btn-white btn-lg fadeInUpBig animated delay-one-s' href='#'> Show more </a> </div>
          <div class='col-md-6 animate pos-sta hidden-xs hidden-sm'> <img class="img-responsive img-right fadeInUpBig animated delay-one-point-five-s" alt="Man" src="img/slider/student-3.png" /> </div>
        </div>
      </div>
    </li>
  </ul>
</div>
<!--/. Konnect Slider --> 

<!-- Company profile -->
<section>
  <div class="container">
    <div class="row">
      <div class="col-md-12"> 
        <!--Services Heading-->
        <h2 class="section-heading">Who We Are?</h2>
        <div class="template-space"></div>
      </div>
      <div class="col-md-6">
        <h2 class="para-heading">About Us.</h2>
        <p>Curabitur ut est a mi fermentum tristique. Aliquam et ante odio. Donec elementum odio eget ex porta, vel laoreet nisl fermentum. Nam risus purus, hendrerit id placerat sit amet, tempor a urna. Maecenas id quam et dolor facilisis pulvinar.</p>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.</p>
        <a href="AboutUs.aspx" class="service-box-button">View More</a> </div>
      <div class="col-md-6"> <img src="img/students.jpg" class="img-responsive img-hide-sm" alt="Company"> </div>
    </div>
  </div>
</section>

<!-- EduCourse Stats-->
<!--
<section class="light-bg">
  <div class="container">
    <div class="row">
      <div class="col-md-12">
        <h2 class="section-heading">Our Status</h2>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec neque erat, ultrices cursus nisi at, hendrerit tristique.</p>
        <div class="template-space"></div>
      </div>
      <div class="company-stats">
        <div class="col-md-3 col-sm-6">
          <div class="profile-box"> <img src="img/icons/tool.png" alt="icon">
            <h4><span>500+</span> professionals trained</h4>
          </div>
        </div>
        <div class="col-md-3 col-sm-6">
          <div class="profile-box"> <img src="img/icons/expert.png" alt="icon">
            <h4><span>10+ Years</span> of Experience</h4>
          </div>
        </div>
        <div class="col-md-3 col-sm-6">
          <div class="profile-box"> <img src="img/icons/clients.png" alt="icon">
            <h4><span>15+</span> Companies Association</h4>
          </div>
        </div>
        <div class="col-md-3 col-sm-6">
          <div class="profile-box"> <img src="img/icons/success.png" alt="icon">
            <h4><span>99%</span> Job Guarantee</h4>
          </div>
        </div>
      </div>
    </div>
  </div>
</section>
-->
<!--Courses-->
<section class="template-news light-bg">
  <div class="container">
    <div class="row">
      <div class="col-md-12">
        <h2 class="section-heading text-dark">Why Schools Need Our Product</h2>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec neque erat, ultrices cursus nisi at, hendrerit tristique.</p>
        <div class="template-space"></div>
      </div>
    </div>
    <div class="row"> 
      <!--Course One-->
      <div class="col-sm-4 article-box">
        <article>
          <div class="news-post">
            <div class="img-box"><a href="#"><img src="img/news/news1.jpg" alt="it's me Image"></a></div>
            <div class="post-content-text">
              <h4><span>For School</span></h4>
			  <div class="post-more"><a href="ForSchool.aspx">view Detail</a> </div>
            </div>
          </div>
        </article>
      </div>
      
      <!--Course Two-->
      <div class="col-sm-4 article-box">
        <article>
          <div class="news-post">
            <div class="img-box"><a href="#"><img src="img/news/news3.jpg" alt="it's me Image"></a> </div>
            <div class="post-content-text">
              <h4><span>For Teacher</span></h4>
			  <div class="post-more"><a href="ForTeacher.aspx">view Detail</a> </div>
            </div>
          </div>
        </article>
      </div>
      
      <!--Course Three-->
      <div class="col-sm-4 article-box">
        <article>
          <div class="news-post">
            <div class="img-box"><a href="#"><img src="img/news/news2.jpg" alt="it's me Image"></a> </div>
            <div class="post-content-text">
              <h4><span>For Parents</span></h4>
			  <div class="post-more"><a href="ForParents.aspx">view Detail</a> </div>
            </div>
          </div>
        </article>
      </div>
    </div>
  </div>
</section>
<section class="">
  <div class="container ">
  <div class="row">
      <div class="col-md-12">
        <h2 class="section-heading text-dark">PRODUCT KEY FEATURES</h2>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec neque erat, ultrices cursus nisi at, hendrerit tristique.</p>
        <div class="template-space"></div>
      </div>
    </div>
  <div class="row">
    <div class="col-sm-4">
      <div class="banner-bar"> <img src="img/icons/classroom.png" alt="icon">
        <h3><span>Feature 1</span></h3>
        <p>Curabitur ut est a mi fermentum tristique. Aliquam et ante odio. Donec elementum odio eget ex porta, vel laoreet nisl fermentum.</p>
      </div>
    </div>
    <div class="col-sm-4">
      <div class="banner-bar"> <img src="img/icons/classroom.png" alt="icon">
        <h3><span>Feature 2</span></h3>
        <p>Curabitur ut est a mi fermentum tristique. Aliquam et ante odio. Donec elementum odio eget ex porta, vel laoreet nisl fermentum.</p>
      </div>
    </div>
    <div class="col-sm-4">
      <div class="banner-bar"> <img src="img/icons/classroom.png" alt="icon">
        <h3><span>Feature 3</span></h3>
        <p>Curabitur ut est a mi fermentum tristique. Aliquam et ante odio. Donec elementum odio eget ex porta, vel laoreet nisl fermentum.</p>
      </div>
    </div>
  </div>
</div>
</section>
<!--Testmonials -->
<aside class="dark-bg">
  <div class="container">
    <div class="row">
      <div class="col-md-12 text-white">
        <h2 class="section-heading text-white">Reviews</h2>
        <div class="template-space"></div>
      </div>
    </div>
    <div class="row">
      <div class="col-md-12" data-wow-delay="0.2s">
        <div class="carousel slide" data-ride="carousel" id="quote-carousel"> 
          <!-- Carousel Slides / Quotes -->
          <div class="carousel-inner text-center"> 
            <!--Testmonial One active-->
            <div class="item active">
              <blockquote>
                <div class="row">
                  <div class="col-md-8 col-md-offset-2 col-xs-12">
                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. !</p>
                    <small>Someone Client</small>
					</div>
                </div>
              </blockquote>
            </div>
            
            <!--Testmonial Two-->
            <div class="item">
              <blockquote>
                <div class="row">
                  <div class="col-md-8 col-md-offset-2 col-xs-12">
                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. </p>
                    <small>Someone Client</small> </div>
                </div>
              </blockquote>
            </div>
            
            <!--Testmonial Three-->
            <div class="item">
              <blockquote>
                <div class="row">
                  <div class="col-md-8 col-md-offset-2 col-xs-12">
                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. .</p>
                    <small>Someone Client</small> </div>
                </div>
              </blockquote>
            </div>
          </div>
          
          <!-- Carousel Buttons Next/Prev --> 
          <a data-slide="prev" href="#quote-carousel" class="left carousel-control"><i class="fa fa-angle-left"></i></a> <a data-slide="next" href="#quote-carousel" class="right carousel-control"><i class="fa fa-angle-right"></i></a> 
          <!-- Bottom Carousel Indicators -->
          <ol class="carousel-indicators">
            <li data-target="#quote-carousel" data-slide-to="0" class="active"></li>
            <li data-target="#quote-carousel" data-slide-to="1"></li>
            <li data-target="#quote-carousel" data-slide-to="2"></li>
          </ol>
        </div>
      </div>
    </div>
  </div>
</aside>

<!-- Gallery Box-->
<section class="template-news">
  <div class="container">
    <div class="row">
      <div class="col-md-12">
        <h2 class="section-heading text-dark">Gallery</h2>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec neque erat, ultrices cursus nisi at, hendrerit tristique.</p>
        <div class="template-space"></div>
      </div>
    </div>
    <div class="row"> 
      <!--Gallery image-->
      <div class="col-sm-3 gallery-box"> <a href="#" data-toggle="modal" data-target=".pop-up-1"> <img src="img/gallery/gallery-one.jpg" class="img-responsive center-block" alt=""> </a> </div>
      <!--Gallery image-->
      <div class="col-sm-3 gallery-box"> <a href="#" data-toggle="modal" data-target=".pop-up-2"> <img src="img/gallery/gallery-two.jpg" class="img-responsive center-block" alt=""> </a> </div>
      <!--Gallery image-->
      <div class="col-sm-3 gallery-box"> <a href="#" data-toggle="modal" data-target=".pop-up-3"> <img src="img/gallery/gallery-three.jpg" class="img-responsive center-block" alt=""> </a> </div>
      <!--Gallery image-->
      <div class="col-sm-3 gallery-box"> <a href="#" data-toggle="modal" data-target=".pop-up-4"> <img src="img/gallery/gallery-four.jpg" class="img-responsive center-block" alt=""> </a> </div>
      
      <!--  Modal content one-->
      <div class="modal fade pop-up-1" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-body">
              <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
              <img src="img/gallery/gallery-one.jpg" class="img-responsive center-block" alt=""> </div>
          </div>
        </div>
      </div>
      <!-- /.modal image --> 
      <!--  Modal content two-->
      <div class="modal fade pop-up-2" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-body">
              <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
              <img src="img/gallery/gallery-two.jpg" class="img-responsive center-block" alt=""> </div>
          </div>
        </div>
      </div>
      <!-- /.modal image --> 
      <!--  Modal content three-->
      <div class="modal fade pop-up-3" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-body">
              <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
              <img src="img/gallery/gallery-three.jpg" class="img-responsive center-block" alt=""> </div>
          </div>
        </div>
      </div>
      <!-- /.modal image --> 
      <!--  Modal content four-->
      <div class="modal fade pop-up-4" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-body">
              <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
              <img src="img/gallery/gallery-four.jpg" class="img-responsive center-block" alt=""> </div>
          </div>
        </div>
      </div>
      <!-- /.modal image --> 
      
    </div>
    <!-- /.row --> 
  </div>
</section>


</asp:Content>
