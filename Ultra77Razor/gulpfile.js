const gulp = require('gulp');
const sass = require('gulp-sass')(require('sass'));
const browserSync = require('browser-sync').create();

function style(){
    return gulp.src('./wwwroot/scss/**/*.scss')
    .pipe(sass())
    .pipe(gulp.dest('./wwwroot/css/'))
    .pipe(browserSync.stream());
}

function watch(){
    browserSync.init({
        server:{
            baseDir: './'
        }
    })
    gulp.watch('./wwwroot/scss/**/*.scss', style);
    gulp.watch('./*.html').on('change',
    browserSync.reload);
}

exports.watch = watch;
exports.style = style;