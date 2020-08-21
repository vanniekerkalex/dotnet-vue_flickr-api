<template>
  <div id="app">
    <img alt="Vue logo" src="./assets/logo.png" style="width: 70px;">
    <h1>P I X P L O R E R</h1>
    <!-- <input @keyup.enter="tags.length > 0 ? searchImages : loadRandomImages" v-model="tags" /> -->
    <!-- <input v-on="{ '@keyup.enter': () => tags.length > 0 ? searchImages : loadRandomImages }" v-model="tags" /> -->
    <input @keyup.enter="searchKey" v-model="tags" />
    <button :disabled="tags.length < 1" @click="searchKey">Search</button>
    <br />
    <div class="section-flickr">
      <ul class="flickr-showcase clearfix">
        <li v-for="url in urls" :key="url">
          <figure class="flickr-photo">
            <img :src="url" style="" >
          </figure>
        </li>
      </ul>
    </div>
    <!-- <div>
      <ul >
        <li v-for="url in urls" :key="url">
          <img
            :src="url"
          >
        </li>
      </ul>
    </div> -->
  </div>
</template>

<script>
import axios from 'axios'

export default {
	name: 'App',
	components: {
	},
	data() {
		return {
			data: "",
			photos: "",
			urls: [],
			errors: [],
			tags: "",
		}
	},
	created() {
		this.loadRandomImages()
	},
	mounted() {

	},
	methods: {
		searchKey() {
			(this.tags.length > 0) ? this.searchImages() : this.loadRandomImages()
		},
		loadRandomImages() {
			console.log("Getting random images.")
			axios.get(`http://localhost:5000/images`, {
				headers: {
					// remove headers
				}
			})
				.then(response => {
					// JSON responses are automatically parsed.
					var data = response.data
					this.data = JSON.parse(data.substring(14, data.length - 1))
					this.photos = this.data.photos["photo"]
					this.generateImageUrls()
				})
				.catch(e => {
					this.errors.push(e)
				})
		},
		generateImageUrls() {
			const urls = []
			for (const [, photo] of Object.entries(this.photos)) {
				if (photo['farm'] > 1 && photo['server'] > 1 && photo['id'] > 1) {
					var url = 'https://farm{farm-id}.staticflickr.com/{server-id}/{id}_{secret}.png'
					url = url.replace(/{farm-id}/, photo['farm'])
					url = url.replace(/{server-id}/, photo['server'])
					url = url.replace(/{id}/, photo['id'])
					url = url.replace(/{secret}/, photo['secret'])
					urls.push(url)
				}
			}
			this.urls = urls
		},
		searchImages() {
			console.log("Searching:", this.tags)
			axios.post(`http://localhost:5000/images`, {
				headers: {
					// remove headers
				}
			}, {
				params: {
					tags: this.tags
				}
			})
				.then(response => {
					// JSON responses are automatically parsed.
					var data = response.data
					this.data = JSON.parse(data.substring(14, data.length - 1))
					this.photos = this.data.photos["photo"]
					this.generateImageUrls()
				})
				.catch(e => {
					this.errors.push(e)
				})
		}
	},
}
</script>

<style>
#app {
	font-family: Avenir, Helvetica, Arial, sans-serif;
	-webkit-font-smoothing: antialiased;
	-moz-osx-font-smoothing: grayscale;
	text-align: center;
	color: #2c3e50;
	margin-top: 60px;
	min-width: 320px;
}

ul {
	list-style-type: none;
	padding: 0;
}

li {
	display: inline-block;
	/* margin: 0 10px; */
}

/* .section-flickr {
	text-align: center;
	padding: 0;
} */

/* .flickr-showcase {
	list-style: none;
	width: 100%;
} */

/* .flickr-showcase li {
	display: block;
	float: left;
	width: 25%;
} */

.flickr-photo {
	transform: scale(0.9);
	width: 100%;
	margin: 0;
	overflow: hidden;
	background-color: #000;
	vertical-align: middle;
}

.flickr-photo img {
	opacity: 0.7;
	/* width: 100%; */
	width: 300px;
	height: auto;
	transform: scale(1.05);
	transition: transform 0.5s, opacity 0.5s;
	/* width: 300px; */
}

.flickr-photo img:hover {
	transform: scale(1.15);
	opacity: 1;
}
</style>
