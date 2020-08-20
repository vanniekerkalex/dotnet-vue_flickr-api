<template>
  <div id="app">
    <img alt="Vue logo" src="./assets/logo.png">
    <!-- <HelloWorld msg="Welcome to Your Vue.js App"/> -->
    <p>{{ this.posts }}</p>
  </div>
</template>

<script>
import axios from 'axios'
// import HelloWorld from './components/HelloWorld.vue'

export default {
	name: 'App',
	components: {
		// HelloWorld
	},
	data() {
		return {
			posts: [],
			errors: []
		}
	},
	created() {
		axios.get(`http://localhost:5000/images`, {
			headers: {
				// remove headers
			}
		})
			.then(response => {
				// JSON responses are automatically parsed.
				this.posts = response
			})
			.catch(e => {
				this.errors.push(e)
			})
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
}
</style>
