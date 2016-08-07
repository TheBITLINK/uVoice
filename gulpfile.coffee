browserify = require('browserify')
express = require('express')
gulp = require('gulp')
jade = require('gulp-jade')
morgan = require('morgan')
less = require('gulp-less')
serveStatic = require('serve-static')
source = require('vinyl-source-stream')
watchify = require('watchify')
autoprefixer = require('gulp-autoprefixer')

buildBrowserify = ->
  browserify({
    cache: {}
    packageCache: {}
    fullPaths: true
    entries: [
      './app/main.coffee'
    ]
    extensions: [ '.js', '.coffee', '.jade' ]
    debug: true
  })
    .transform('coffeeify')
    .transform('jadeify')

gulp.task 'browserify', ->
  buildBrowserify()
    .plugin('minifyify', map: 'main.js', output: './dist/main.js.map')
    .bundle()
    .on('error', console.warn)
    .pipe(source('main.js'))
    .pipe(gulp.dest('./app/dist/'))

gulp.task 'browserify-dev', ->
  bundler = watchify(buildBrowserify(), watchify.args)

  rebundle = ->
    bundler.bundle()
      .on('error', console.warn)
      .pipe(source('main.js'))
      .pipe(gulp.dest('./app/dist/'))

  bundler.on('update', rebundle)
  rebundle()

gulp.task 'jade', ->
  gulp.src('./app/views/**/*.jade')
    .pipe(jade())
    .pipe(gulp.dest('./app/dist/'))

gulp.task 'less', ->
  gulp.src('./app/assets/less/main.less')
    .pipe(less())
    .pipe(autoprefixer({
            browsers: ['last 2 versions'],
            cascade: false
        }))
    .pipe(gulp.dest('./app/dist/'))

gulp.task 'dev', [ 'browserify-dev', 'jade', 'less' ], ->
  gulp.watch('./app/assets/less/**/*.less', [ 'less' ])
  gulp.watch('./app/views/**/*.jade', [ 'jade' ])

gulp.task 'dist', [ 'browserify', 'jade', 'less' ]

gulp.task 'default', [ 'dev' ], ->
  # Start a web server
  app = express()
  app.use(morgan('dev'))
  app.use(serveStatic('app', {
    extensions: [ 'html' ]
    setHeaders: (res) -> res.setHeader('Access-Control-Allow-Origin', '*')
  }))
  app.listen(8000)
  console.log('Serving at http://localhost:8000')
