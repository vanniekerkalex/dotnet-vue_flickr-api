<template>
  <div id="app">
    <a href="#" @click="loadRandomImages"><img alt="Vue logo" src="./assets/logo.png" style="width: 70px;"></a>
    <h1>P I X P L O R E R</h1>
    <input @keyup.enter="searchKey" v-model="tags" />
    <button :disabled="tags.length < 1" @click="searchKey">Search</button>
    <br />
    <div class="section-flickr">
			<h3 v-if="errorMsg.length > 0">{{ errorMsg }}</h3>
      <ul class="flickr-showcase">
        <li v-for="url in urls" :key="url">
          <figure class="flickr-photo">
            <img @click="openImage(url)" :src="url" style="" >
          </figure>
        </li>
      </ul>
    </div>
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
			errorMsg: ""
		}
	},
	created() {
		this.loadRandomImages()
	},
	mounted() {

	},
	methods: {
		searchKey() {
			this.urls = "";
			(this.tags.length > 0) ? this.searchImages() : this.loadRandomImages()
		},
		loadRandomImages() {
			console.log("Getting random images.")
			this.tags = ""
			this.errorMsg = ""
			axios.get(`http://localhost:5000/images`, {
				headers: {}
			})
				.then(response => {
					var data = response.data
					this.data = JSON.parse(data.substring(14, data.length - 1))
					this.photos = this.data.photos["photo"]
					this.generateImageUrls()
				})
				.catch(e => {
					this.errors.push(e)
					this.error()
				})
		},
		searchImages() {
			console.log("Searching:", this.tags)
			this.errorMsg = ""
			axios.post(`http://localhost:5000/images`, {
				headers: {}
			}, {
				params: {
					tags: this.tags
				}
			})
				.then(response => {
					var data = response.data
					this.data = JSON.parse(data.substring(14, data.length - 1))
					this.photos = this.data.photos["photo"]
					this.generateImageUrls()
				})
				.catch(e => {
					this.errors.push(e)
					this.error()
				})
		},
		generateImageUrls() {
			const urls = []
			for (const [, photo] of Object.entries(this.photos)) {
				if (photo['farm'] > 0 && photo['server'] > 0 && photo['id'] > 0) {
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
		openImage(url) {
			window.open(url, "_blank")
		},
		error() {
			this.urls = ""
			this.errorMsg = "Sorry, there is an error!"
			console.log(this.errorMsg)
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
}

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
	width: 300px;
	height: auto;
	transform: scale(1.05);
	transition: transform 0.5s, opacity 0.5s;
}

.flickr-photo img:hover {
	transform: scale(1.15);
	opacity: 1;
}
</style>
