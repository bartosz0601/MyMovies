<script>
import "bootstrap/dist/css/bootstrap.min.css"
import "bootstrap"
import { Toast } from "bootstrap"

export default {
    props:['show'],
    watch: {
        show(){
            var toast = new Toast(this.$refs.el);
            toast.show();
        }
    },
}
</script>
<template>
    <div class="toast-container position-fixed bottom-0 end-0 p-3">
        <div ref="el" class="toast text-bg-warning" role="alert" aria-live="assertive"
            aria-atomic="true">
            <div class="toast-header">
                <strong class="me-auto">Error</strong>
                <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
            </div>
            <div class="toast-body">
                <slot></slot>
            </div>
        </div>
    </div>
</template>
<style>

</style>