/// <binding BeforeBuild='build' />
const { src, dest, series, parallel } = require('gulp');
const rename = require('gulp-rename');
const uglify = require('gulp-uglify');
const sass = require('gulp-sass')(require('sass'));
const sourcemaps = require('gulp-sourcemaps');
const csso = require('gulp-csso');
const replace = require('gulp-replace');
const del = require('del');

// Clean the wwwroot folder
function clean() {
    var del = require('del');
    return del(['./wwwroot/**', '!./wwwroot/placeholder.txt']);
}

//module pack files
function scripts() {
    return src('./pack/src/js/*.js', { encoding: false })
        .pipe(dest('./wwwroot/js/'))
        .pipe(uglify())
        .pipe(rename({ extname: '.min.js' }))
        .pipe(dest('./wwwroot/js/'));
}

exports.clean = clean;
exports.build = series(clean, scripts);