<template>
  <section>
    <div class="section-one">
      <div class="section-one-left">
        <p class="lead">
          fresh food for fresher you
        </p>
        <p class="subtitle">
          <i>Quality food, quality life.</i>
        </p>
      </div>
      <div class="section-one-right">
        <img src="@/assets/res/Screen 2/Side image.png">
      </div>
    </div>
    <div class="section-two">
      <div class="card">
        <img src="@/assets/res/Screen 2/clock.png" />
        <span>Online anywhere</span>
      </div>
      <div class="card">
        <img src="@/assets/res/Screen 2/delivery-truck.png" />
        <span>Fast delivery</span>
      </div>
      <div class="card">
        <img src="@/assets/res/Screen 2/support.png" />
        <span>24/7 Support</span>
      </div>
    </div>
  </section>
</template>

<style scoped>
section {
  padding: 5rem 10vw;
  max-width: 100vw;
  overflow: hidden;
}
.section-one {
  display: grid;
  grid-template-areas: 'left right';
  grid-template-columns: 1fr 1fr;
}

.section-one-left {
  grid-area: left;
  display: block;
}
.section-one-right {
  grid-area: right;
  display: block;
}

.section-one-right img {
  width: 100%;
}
p.lead {
  font-family: "HelveticaBold";
  font-size: 60pt;
  text-transform: uppercase;
  line-height: 5rem;
  margin: 5rem 0;
}
p.subtitle {
  font-family: "HelveticaMedium";
  font-size: 36pt;
}
.section-two {
  display: grid;
  grid-template-columns: repeat(3, minmax(200px, 1fr));
}
.card {
  position: relative;
  display: flex;
  flex-direction: column;
  min-width: 0;
  padding: 5rem;
}
.card img {
  width: 100%;
  height: 100%;
  object-fit: contain;
}
.card span {
  margin-top: 3rem;
  text-align: center;
  font-family: "HelveticaMedium";
  font-size: 30pt;
  font-style: italic;
}
</style>
