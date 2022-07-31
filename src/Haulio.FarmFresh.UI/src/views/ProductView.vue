<script>
let data = null;

export default {
  name: "ProductView",
  data() {
    return {
      menuData: null,
      selectedMenu: null,
      isProductDetailShown: false,
      selectedProductDetail: null,
      isReadMore: false
    }
  },
  methods: {
    async getData() {
      
      try {
        const res = await fetch("https://localhost:5001/api/v1/productmenu", {
          method: 'GET',
          mode: 'cors'
        });
        if (!res.ok) {
          throw new Error(`An error has occured ${res.status} - ${res.statusText}`);
        }
        const data = await res.json();
        this.menuData = data.data.sort((a,b) => a.position - b.position);
        console.log(this.menuData);
      } catch (err) {
        console.error(err);
      }
    },
    onProductSelected(product) {
      this.selectedProductDetail = product;
      this.isProductDetailShown = true;
    },
    onExitProductDetail() {
      this.isProductDetailShown = false;
      this.isReadMore = false;
    },
    onMenuClicked(e) {
      console.log(e);
    }
  },
  async mounted(){
    await this.getData();
  }
}

</script>
<template>
  <section>
    <aside>
      <ul v-if="menuData">
        <li v-for="menu in menuData" :key="menu.id" @click="selectedMenu = menu" :class="{active: menu.id === selectedMenu?.id}">
          <a>{{menu.displayText}}</a>
          <ul class="submenu">
            <li><a>Submenu 1</a></li>
            <li><a>Submenu 2</a></li>
          </ul>
        </li>
      </ul>
    </aside>
    <div class="product-list-container" v-if="selectedMenu">
      <div class="card" v-for="product in selectedMenu.products" @click="onProductSelected(product)">
        <img v-bind:src="product.imageUrls?.[0] ?? '/src/assets/default-product-img.jpg'" />
        <div>
          <span class="product-name">
            {{product.name}}
          </span>
          <span class="product-desc">
            {{product.strategy}}
          </span>
        </div>
      </div>
      <div v-if="isProductDetailShown" class="product-popup" @click="onExitProductDetail">
        <div class="product-detail-wrapper" @click.prevent.stop>
          <img v-bind:src="selectedProductDetail.imageUrls[0]" />
          <div class="product-detail">
            <span class="product-detail-title">
              {{selectedProductDetail.name}}
            </span>
            <span class="product-detail-strategy">
              {{selectedProductDetail.strategy}}
            </span>
            <table class="product-detail-other">
              <tr>
                <td>Key Information</td>
                <td>{{selectedProductDetail.strategy}}</td>
              </tr>
              <tr>
                <td>About the Product</td>
                <td v-if="isReadMore">{{selectedProductDetail.description}}</td>
                <td v-else="isReadMore">
                  {{selectedProductDetail.description.substring(0,50)}}...
                  <span class="read-more" @click="isReadMore=true">More</span>
                </td>
              </tr>
              <tr>
                <td>Country of Origin</td>
                <td>France</td>
              </tr>
            </table>
            <div class="add-tocart-container">
              <span class="style-red">Qty</span>
              <input type="number" value="1"/>
              <button class="btn-add-tocart style-red" type="button">ADD TO CART</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<style scoped>
section {
  height: 100vh;
  width: 100%;
  background-color: rgb(235 235 235);
  display: grid;
  grid-template-columns: 20vw 1fr;
}

aside {
    height: 100%;
    width: 100%;
    justify-content: space-between;
    flex-direction: column;
    background-color: rgb(236, 240, 241);
    z-index: 999;
}
aside ul {
    list-style-type: none;
    text-align: left;
    margin-block-start: 0;
    padding-inline-start: 0;
}
aside ul a {
    text-decoration: none;
    display: inline-block;
    cursor: pointer;
    font-family: "HelveticaMedium";
    font-size: 21pt;
    color: rgb(102, 102, 102);
}

aside ul li {
  padding: 1rem 4rem;
}
aside ul li.active {
  background-color: rgb(150, 150, 150);
}
aside ul li:hover {
  background-color: rgb(150, 150, 150);
}
aside ul li.active>a{
  color: rgb(236, 240, 241);
}

ul li ul.submenu {
  display: none;
}
ul li.active ul.submenu {
  display: block;
  position: absolute;
  left: 20vw;
  top: 0;
  width: 20vw;
  background-color: inherit;
}
ul li.active ul.submenu a {
  color: rgb(236, 240, 241);
}
.product-list-container {
  display: grid;
  grid-template-columns: repeat(3, minmax(300px, auto));
  padding: 2rem 5rem;
  gap: 3rem;
  background-color: rgb(236, 240, 241);
}
.product-list-container .card {
  position: relative;
  display: flex;
  flex-direction: column;
  max-height: 500px;
}

.product-list-container .card div {
  padding: 0.5rem 0.5rem;
}
.product-list-container .card img {
  width: 100%;
  height: 350px;
  border-radius: 3rem;
  object-fit: cover;
}
.product-list-container .card:hover img {
  filter: brightness(0.8);
}
.product-name {
  color: rgb(102, 102, 102);
  font-family: "HelveticaBold";
  font-size: 21pt;
  display: block;
}
.product-desc {
  color: rgb(75, 75, 75);
  font-family: "HelveticaBold";
  display: block;
  font-size: 21pt;
}
.product-popup {
  position: fixed;
  z-index: 999;
  top: 100px;
  left: 0;
  padding: 5rem;
  padding-left: 30vw;
  width: 100%;
  height: calc(100vh - 100px);
}
.product-detail-wrapper {
  height: 100%;
  padding: 1rem;
  background-color: #fff;
  display: grid;
  grid-template-columns: 40% 1fr;
  border-radius: 1.5rem;
}
.product-detail-wrapper img {
  width: 100%;
  object-fit: cover;
}
.product-detail {
  display: flex;
  flex-direction: column;
  padding-top: 1rem;
  padding-bottom: 3rem;
}
.product-detail span {
  display: block;
}
.product-detail-title {
  font-family: "HelveticaMedium";
  font-size: 36pt;
}
.product-detail-strategy {
  font-family: "HelveticaMedium";
  font-size: 24pt;
  margin-bottom: 1rem;
}

.product-detail-other * {
  font-weight: bold;
  font-family: Arial, Helvetica, sans-serif;
  font-size: 12pt;
  vertical-align: top;
}

.product-detail-other td {
  padding: 0.5rem;
}
.product-detail-other td:first-child{
  min-width: 150px;
}
.product-detail-field {
  margin-bottom: 0.5rem;
}
.product-detail-field *:first-child {
  margin-right: 2.5rem;
}
.product-detail-field span {
  font-family: Arial, Helvetica, sans-serif;
  font-size: 12pt;
  display: inline-block !important;
  font-weight: bold;
}
.read-more {
  cursor: pointer;
  color: coral;
}
.add-tocart-container {
  margin-top: auto;
  font-size: 14pt;
  display: flex;
}
.add-tocart-container input[type=number] {
  max-width: 80px;
  text-align: center;
  border-radius: 0.5rem;
}

.btn-add-tocart {
  width: 150px;
  margin-left: auto;
}

.style-red {
  background: coral;
  padding: 0.3rem 1rem;
  border-radius: 0.3rem;
  color: white;
  border-width: 0 !important;
}

</style>
