<template>
    <div class="img-view">
        <img v-bind:src="imgSrc" />
    </div>
</template>

<script lang="ts">
import {Vue, Component, Prop/* , Watch, Emit, Inject, Model, Provide */} from 'vue-property-decorator'

@Component({})
export default class ImgView extends Vue {
  @Prop({type: String, default: ''}) imgSrc: string = ''
}
</script>

<style scoped>
.img-view{margin-left:auto;margin-right:auto;padding:0.1ex 0.1ex 0.1ex 0.1ex;}
.img-view img{width:100%;}
</style>
