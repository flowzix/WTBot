function Swipe(e, t) {
    "use strict";

    function h() {
        o = s.children, f = o.length, o.length < 2 && (t.continuous = !1), i.transitions && t.continuous && o.length < 3 && (s.appendChild(o[0].cloneNode(!0)), s.appendChild(s.children[1].cloneNode(!0)), o = s.children), u = new Array(o.length), a = e.getBoundingClientRect().width || e.offsetWidth, s.style.width = o.length * a + "px";
        var n = o.length;
        while (n--) {
            var r = o[n];
            r.style.width = a + "px", r.setAttribute("data-index", n), i.transitions && (r.style.left = n * -a + "px", g(n, l > n ? -a : l < n ? a : 0, 0))
        }
        t.continuous && i.transitions && (g(v(l - 1), -a, 0), g(v(l + 1), a, 0)), i.transitions || (s.style.left = l * -a + "px"), w(l, o), e.style.visibility = "visible"
    }

    function p() {
        t.continuous ? m(l - 1) : l && m(l - 1)
    }

    function d() {
        t.continuous ? m(l + 1) : l < o.length - 1 && m(l + 1)
    }

    function v(e) {
        return (o.length + e % o.length) % o.length
    }

    function m(e, n) {
        if (l == e) return;
        if (i.transitions) {
            var s = Math.abs(l - e) / (l - e);
            if (t.continuous) {
                var f = s;
                s = -u[v(e)] / a, s !== f && (e = -s * o.length + e)
            }
            var h = Math.abs(l - e) - 1;
            while (h--) g(v((e > l ? e : l) - h - 1), a * s, 0);
            e = v(e), g(l, a * s, n || c), g(e, 0, n || c), t.continuous && g(v(e - s), -(a * s), 0)
        } else e = v(e), b(l * -a, e * -a, n || c);
        l = e, w(l, o), r(t.callback && t.callback(l, o[l]))
    }

    function g(e, t, n) {
        y(e, t, n), u[e] = t
    }

    function y(e, t, n) {
        var r = o[e],
            i = r && r.style;
        if (!i) return;
        i.webkitTransitionDuration = i.MozTransitionDuration = i.msTransitionDuration = i.OTransitionDuration = i.transitionDuration = n + "ms", i.webkitTransform = "translate(" + t + "px,0)" + "translateZ(0)", i.msTransform = i.MozTransform = i.OTransform = "translateX(" + t + "px)"
    }

    function b(e, n, r) {
        if (!r) {
            s.style.left = n + "px";
            return
        }
        var i = +(new Date),
            u = setInterval(function() {
                var a = +(new Date) - i;
                if (a > r) {
                    s.style.left = n + "px", E && x(), t.transitionEnd && t.transitionEnd.call(event, l, o[l]), clearInterval(u);
                    return
                }
                s.style.left = (n - e) * (Math.floor(a / r * 100) / 100) + e + "px"
            }, 4)
    }

    function w(e, t) {
        var n = t.length;
        while (n--) {
            t[n].style.visibility = "hidden";
            if (n === v(e) || n === v(e - 1) || n === v(e + 1)) t[n].style.visibility = "visible"
        }
    }

    function x() {
        S = setTimeout(d, E)
    }

    function T() {
        E = 0, clearTimeout(S)
    }
    var n = function() {},
        r = function(e) {
            setTimeout(e || n, 0)
        },
        i = {
            addEventListener: !!window.addEventListener,
            touch: "ontouchstart" in window || window.DocumentTouch && document instanceof DocumentTouch,
            transitions: function(e) {
                var t = ["transitionProperty", "WebkitTransition", "MozTransition", "OTransition", "msTransition"];
                for (var n in t)
                    if (e.style[t[n]] !== undefined) return !0;
                return !1
            }(document.createElement("swipe"))
        };
    if (!e) return;
    var s = e.children[0],
        o, u, a, f;
    t = t || {};
    var l = parseInt(t.startSlide, 10) || 0,
        c = t.speed || 300;
    t.continuous = t.continuous !== undefined ? t.continuous : !0;
    var E = t.auto || 0,
        S, N = !1,
        C, k = {},
        L = {},
        A, O = {
            handleEvent: function(e) {
                switch (e.type) {
                    case "gesturechange":
                        this.gesturechange(e);
                        break;
                    case "gesturestart":
                        this.gesturestart(e);
                        break;
                    case "touchstart":
                        this.start(e);
                        break;
                    case "touchmove":
                        this.move(e);
                        break;
                    case "touchend":
                        r(this.end(e));
                        break;
                    case "webkitTransitionEnd":
                    case "msTransitionEnd":
                    case "oTransitionEnd":
                    case "otransitionend":
                    case "transitionend":
                        r(this.transitionEnd(e));
                        break;
                    case "resize":
                        r(h.call())
                }
                t.stopPropagation && e.stopPropagation()
            },
            gesturestart: function(e) {
                C = +(new Date)
            },
            gesturechange: function(e) {
                if (popZoomTriggered) return;
                e.preventDefault(), t.disableScroll && e.preventDefault();
                var n = +(new Date) - C,
                    r = Number(n) > 100;
                e.scale > 1 && r && (popZoomTriggered = !0, $(window).trigger("start-zoom"))
            },
            start: function(e) {
                var t = e.touches[0];
                k = {
                    length: e.touches.length,
                    x: t.pageX,
                    y: t.pageY,
                    time: +(new Date)
                }, A = undefined, L = {}, s.addEventListener("touchmove", this, !1), s.addEventListener("touchend", this, !1)
            },
            move: function(e) {
                if (t.disableSwipe) return;
                if (e.touches.length > 1 || e.scale && e.scale !== 1) return;
                t.disableScroll && e.preventDefault();
                var n = e.touches[0];
                L = {
                    x: n.pageX - k.x,
                    y: n.pageY - k.y
                }, typeof A == "undefined" && (A = !!(A || Math.abs(L.x) < Math.abs(L.y))), A || (e.preventDefault(), T(), t.continuous ? (y(v(l - 1), L.x + u[v(l - 1)], 0), y(l, L.x + u[l], 0), y(v(l + 1), L.x + u[v(l + 1)], 0)) : (L.x = L.x / (!l && L.x > 0 || l == o.length - 1 && L.x < 0 ? Math.abs(L.x) / a + 1 : 1), y(l - 1, L.x + u[l - 1], 0), y(l, L.x + u[l], 0), y(l + 1, L.x + u[l + 1], 0)))
            },
            end: function(e) {
                var n = +(new Date) - k.time,
                    r = Number(n) < 250 && Math.abs(L.x) > 20 || Math.abs(L.x) > a / 2,
                    i = !l && L.x > 0 || l == o.length - 1 && L.x < 0;
                t.continuous && (i = !1);
                var f = L.x < 0;
                A || (r && !i ? (f ? (t.continuous ? (g(v(l - 1), -a, 0), g(v(l + 2), a, 0)) : g(l - 1, -a, 0), g(l, u[l] - a, c), g(v(l + 1), u[v(l + 1)] - a, c), l = v(l + 1)) : (t.continuous ? (g(v(l + 1), a, 0), g(v(l - 2), -a, 0)) : g(l + 1, a, 0), g(l, u[l] + a, c), g(v(l - 1), u[v(l - 1)] + a, c), l = v(l - 1)), w(l, o), t.callback && t.callback(l, o[l])) : t.continuous ? (g(v(l - 1), -a, c), g(l, 0, c), g(v(l + 1), a, c)) : (g(l - 1, -a, c), g(l, 0, c), g(l + 1, a, c))), s.removeEventListener("touchmove", O, !1), s.removeEventListener("touchend", O, !1)
            },
            transitionEnd: function(e) {
                parseInt(e.target.getAttribute("data-index"), 10) == l && (E && x(), t.transitionEnd && t.transitionEnd.call(e, l, o[l]))
            }
        };
    return h(), E && x(), i.addEventListener ? (i.touch && s.addEventListener("touchstart", O, !1), i.touch && s.addEventListener("gesturechange", O, !1), i.touch && s.addEventListener("gesturestart", O, !1), i.transitions && (s.addEventListener("webkitTransitionEnd", O, !1), s.addEventListener("msTransitionEnd", O, !1), s.addEventListener("oTransitionEnd", O, !1), s.addEventListener("otransitionend", O, !1), s.addEventListener("transitionend", O, !1)), window.addEventListener("resize", O, !1)) : window.onresize = function() {
        h()
    }, {
        setup: function() {
            h()
        },
        slide: function(e, t) {
            T(), m(e, t)
        },
        prev: function() {
            T(), p()
        },
        next: function() {
            T(), d()
        },
        getPos: function() {
            return l
        },
        getNumSlides: function() {
            return f
        },
        kill: function() {
            T(), s.style.width = "auto", s.style.left = 0;
            var e = o.length;
            while (e--) {
                var t = o[e];
                t.style.width = "100%", t.style.left = 0, i.transitions && y(e, 0, 0)
            }
            i.addEventListener ? (s.removeEventListener("touchstart", O, !1), s.removeEventListener("webkitTransitionEnd", O, !1), s.removeEventListener("msTransitionEnd", O, !1), s.removeEventListener("oTransitionEnd", O, !1), s.removeEventListener("otransitionend", O, !1), s.removeEventListener("transitionend", O, !1), window.removeEventListener("resize", O, !1)) : window.onresize = null
        }
    }
}

function defaultRoute() {
    return $("body").hasClass("for-native-checkout") ? "checkout" : "categories"
}

function productDetailViewPoller(e) {
    var t = currentRoute(),
        n = t.params;
    if (!_.isUndefined(t) && !_.isUndefined(t.route) && !_.isUndefined(t.params) && t.route == "productDetail" && e == parseInt(n[0])) {
        var r = Supreme.getProductOverviewDetailsForId(e, allCategoriesAndProducts),
            i = new Product({
                id: e,
                name: r.name,
                price: r.price,
                price_euro: r.price_euro,
                sale_price: r.sale_price,
                sale_price_euro: r.sale_price_euro,
                categoryName: r.categoryName
            });
        i.fetch({
            success: function(n, r, s) {
                markItemTimeViewed(e);
                var o = JSON.stringify(i.get("styles").map(function(e) {
                        return e.get("sizes").map(function(e) {
                            return e.get("stock_level") == 0
                        })
                    })),
                    u = JSON.stringify(productDetailView.model.get("styles").map(function(e) {
                        return e.get("sizes").map(function(e) {
                            return e.get("stock_level") == 0
                        })
                    })),
                    a = t.params;
                e == parseInt(a[0]) && u != o && !_.isUndefined(t) && !_.isUndefined(t.route) && !_.isUndefined(t.params) && t.route == "productDetail" && a.length >= 1 && (console.log("product poll reload"), Supreme.app.productDetail(e))
            }
        })
    }
}

function TrackTiming(e, t, n) {
    return this.category = e, this.variable = t, this.label = n ? n : undefined, this.startTime, this.endTime, this
}

function setCurrentLang(e) {
    createCookie("lang", e, 1e5), createCookie("langChanged", 1, 1e5)
}

function currentLang() {
    var e = "en";
    return readCookie("lang") == null ? e : readCookie("lang")
}

function showLanguageSetter(e, t) {
    var n = $('<div id="language-bg" style="z-index:2;opacity:0;position:fixed;top:0;bottom:0;left:0;right:0;background:rgba(0, 0, 0, .25);"></div>');
    $("body").append(n);
    var r = $('<ul id="language-setter"><li class="en">UK</li><li class="de">DE</li><li class="fr">FR</li></ul>');
    r.attr("class", currentLang()), $("body").append(r);
    var i = $("#language-setter");
    i.css({
        position: "fixed",
        top: "50%",
        left: "50%",
        marginLeft: -i.width() / 2,
        marginTop: -i.height() / 2
    }), n.animate({
        opacity: 100
    }, {
        duration: 200,
        easing: fadeEasingType
    }), r.animate({
        opacity: 100
    }, {
        duration: 200,
        easing: fadeEasingType
    }), $("html").addClass("lang-changing"), $(n).click(function(e) {
        hideLanguageSetter(), e.preventDefault()
    }), i.find("li").click(function(e) {
        e.stopImmediatePropagation();
        var t = $(this).attr("class").toLowerCase();
        setCurrentLang(t), setCurrentLangToggle(t), _.delay(function() {
            window.location.reload()
        }, 20)
    })
}

function setCurrentLangToggle(e) {
    var t = e.toUpperCase();
    t == "EN" && (t = "UK"), $("#current-lang").attr("class", e.toLowerCase()).text(t)
}

function hideLanguageSetter() {
    $("html").removeClass("lang-changing"), $("#language-bg").animate({
        opacity: 0
    }, {
        duration: 200,
        easing: fadeEasingType,
        complete: function() {
            $(this).remove()
        }
    }), $("#language-setter").animate({
        opacity: 0
    }, {
        duration: 200,
        easing: fadeEasingType,
        complete: function() {
            $(this).remove()
        }
    })
}

function getNextProductFromId(e) {
    var t, n = _currentCategoryPlural;
    _.isUndefined(_currentCategoryPlural) && (_currentCategoryPlural = "new");
    var r = allCategoriesAndProducts.products_and_categories[_currentCategoryPlural];
    for (var i = 0; i < r.length; i++) r[i].id == e && (_.isUndefined(r[i + 1]) ? t = r[0] : t = r[i + 1]);
    return t
}

function GBPtoEuro(e) {
    return GBP_TO_EURO * e
}

function formatCurrency(e, t, n) {
    var r;
    return e /= 100, e = _.isNaN(e) || e === "" || e === null ? 0 : e, e % 1 == 0 ? r = addCommas(parseFloat(e).toFixed(0)) : r = addCommas(parseFloat(e).toFixed(2)), !_.isUndefined(n) && n && r.indexOf(",") == -1 && (r = Math.round(r)), _.isUndefined(t) ? IS_JAPAN ? "ÂĽ" + r : IS_EU ? (LANG.currency == "eur" ? "âŹ" : "ÂŁ") + r : "$" + r : t.length > 1 ? r + " " + t : t + r
}

function addCommas(e) {
    e += "";
    var t = e.split("."),
        n = t[0],
        r = t.length > 1 ? "." + t[1] : "",
        i = /(\d+)(\d{3})/;
    while (i.test(n)) n = n.replace(i, "$1,$2");
    return n + r
}

function singularCategoryName(e) {
    return singularizedCategories[e]
}

function showLoader() {
    var e = $('<div id="main-loader"></div>'),
        t = $("#container").offset();
    e.css({
        top: t.top,
        left: t.left,
        width: t.width,
        height: t.height
    }), $("#container").before(e), e.animate({
        opacity: 1
    }, fadeSpeed, fadeEasingType)
}

function hideLoader() {
    $("#main-loader").animate({
        opacity: 0
    }, fadeSpeed, fadeEasingType, function() {
        $("#main-loader").remove()
    })
}

function createCookie(e, t, n) {
    if (n) {
        var r = new Date;
        r.setTime(r.getTime() + n * 24 * 60 * 60 * 1e3);
        var i = "; expires=" + r.toGMTString()
    } else var i = "";
    document.cookie = e + "=" + t + i + "; path=/"
}

function readCookie(e) {
    var t = e + "=",
        n = document.cookie.split(";");
    for (var r = 0; r < n.length; r++) {
        var i = n[r];
        while (i.charAt(0) == " ") i = i.substring(1, i.length);
        if (i.indexOf(t) == 0) return i.substring(t.length, i.length)
    }
    return null
}

function eraseCookie(e) {
    createCookie(e, "", -1)
}

function isNamePrintable(e) {
    return !_.isUndefined(e) && !_.isNull(e) && e != "N/A" && e.replace(/^\s+|\s+$/g, "") != "" && e != "&nbsp;"
}

function getNumberOfJumps() {
    var e = localStorage.getItem("numberOfJumps");
    if (_.isNull(e)) return localStorage.setItem("numberOfJumps", 0), 0
}

function getItemTimeViewed(e) {
    return localStorage.getItem(e + "_updated_at")
}

function markItemTimeViewed(e) {
    localStorage.setItem(e + "_updated_at", Date.now())
}

function itemViewIsStale(e) {
    var t = getItemTimeViewed(e);
    return t ? Date.now() - t >= _staleDataAge : !1
}

function incrementNumberOfJumps() {
    var e = localStorage.getItem("numberOfJumps");
    _.isNull(e) ? localStorage.setItem("numberOfJumps", 0) : localStorage.setItem("numberOfJumps", e + 1)
}

function goLastPlace(e) {
    return _.isUndefined(window.navigator.standalone) || !window.navigator.standalone ? !1 : _.isNull(sessionStorage.getItem("hasTriggeredRoute")) ? (sessionStorage.setItem("hasTriggeredRoute", 1), _.isNull(getLastVisitedFragment()) ? !1 : getLastVisitedFragment() == Backbone.history.fragment ? !1 : (e.navigate(getLastVisitedFragment(), {
        trigger: !0
    }), !0)) : !1
}

function showCartAndCheckout() {
    Backbone.history.fragment != "checkout" && ($("#goto-cart-link").show().text(Supreme.app.cart.length()).removeClass("edit"), $("#checkout-now").show()), Backbone.history.fragment != "cart" && $("footer").hasClass("first-loaded") && $("footer").css("opacity", 1)
}

function prefetchImage(e) {
    var t = new Image;
    $(t).attr("src", e)
}

function storageKeyIsProduct(e) {
    return e == "lastMobileApiUpdate" || e == "_cb_cp" || e == "home" || e == "lastFragment" || e == "shoppingSessionId" || e.indexOf("updated_at") !== -1 || e.indexOf("_qty") !== -1 ? !1 : !0
}

function setSessionIDs() {
    var e = (new Date).getTime();
    createCookie("shoppingSessionId", e, 1), localStorage.setItem("shoppingSessionId", e)
}

function setLastVisitedFragment() {
    createCookie("lastVisitedFragment", Backbone.history.fragment, 182)
}

function getLastVisitedFragment() {
    return readCookie("lastVisitedFragment")
}

function set_local_storage(e) {
    for (var t in e) e.hasOwnProperty(t) && localStorage.setItem(t, JSON.stringify(e[t]))
}

function observeFooterLinks() {
    $("footer ul li span").bind("click", function() {
        if ($(this).attr("id") == "lookbook-footer-link") Backbone.history.fragment != "lookbook" ? $.scrollTo(0, 0, function() {
            Supreme.app.navigate("lookbook", {
                trigger: !0
            })
        }) : $.scrollTo(0, scrollSpeed, function() {
            Supreme.app.navigate("lookbook", {
                trigger: !0
            })
        });
        else {
            var e = this;
            $.scrollTo(0, scrollSpeed, function() {
                var t = new StaticContentView;
                t.render($(e).attr("id")), $("html").addClass("static-view")
            })
        }
    })
}

function logError(e, t) {
    if (window.trackJs) var n = $.extend({
        type: "mobileChargeError"
    }, t);
    n = $.extend({
        errorMessage: e
    }, n), $.ajax({
        type: "GET",
        url: "http://app.supremenewyork.com/mobile_error",
        dataType: "jsonp",
        data: n
    })
}

function currentRoute() {
    var e = Supreme.app,
        t = Backbone.history.fragment,
        n = _.pairs(e.routes),
        r = null,
        i = null,
        s;
    return s = _.find(n, function(n) {
        return r = _.isRegExp(n[0]) ? n[0] : e._routeToRegExp(n[0]), r.test(t)
    }), s && (i = e._extractParameters(r, t), r = s[1]), {
        route: r,
        fragment: t,
        params: i
    }
}

function isHiRes() {
    var e = window.devicePixelRatio || window.screen.deviceXDPI / window.screen.logicalXDPI || 1;
    return e > 1
}

function setHiResClass() {
    isHiRes() && ($("body").addClass("hi-res"), $(window).width() > 320 && $("body").addClass("wide"))
}

function isHiResAndWide() {
    return isHiRes() && $(window).width() > 320
}

function median(e) {
    e.sort(function(e, t) {
        return e - t
    });
    var t = Math.floor(e.length / 2);
    return e.length % 2 ? e[t] : (e[t - 1] + e[t]) / 2
}

function loadDataForPoll(e) {
    if (SHOP_CLOSED) return;
    $.getJSON("/mobile_stock.json", function(t, n, r) {
        var i = !1;
        typeof window.splayver == "undefined" ? r.getResponseHeader("X-Splay-Version") != null && r.getResponseHeader("X-Splay-Version").match(/^[0-9a-f]{16}$/) != null && r.getResponseHeader("X-Splay-Version").match(/^[0-9a-f]{16}$/).length == 1 && r.getResponseHeader("X-Splay-Version").match(/^[0-9a-f]{16}$/)[0] == r.getResponseHeader("X-Splay-Version") && (window.splayver = r.getResponseHeader("X-Splay-Version")) : r.getResponseHeader("X-Splay-Version") != null && r.getResponseHeader("X-Splay-Version") != window.splayver && r.getResponseHeader("X-Splay-Version").match(/^[0-9a-f]{16}$/) != null && r.getResponseHeader("X-Splay-Version").match(/^[0-9a-f]{16}$/).length == 1 && r.getResponseHeader("X-Splay-Version").match(/^[0-9a-f]{16}$/)[0] == r.getResponseHeader("X-Splay-Version") && (window.splayver = r.getResponseHeader("X-Splay-Version"), i = !0, location.reload());
        if (!i) {
            window.release_week = t.release_week, window.release_date = t.release_date, handleApiTimestamp(t.last_mobile_api_update);
            if (!_.isUndefined(t.products_and_categories)) {
                t.discount && $("#discount_banner").length == 0 && $("#main").before('<p id="discount_banner">' + t.discount_blurb + "</p>"), SALE_VISIBLE = t.on_sale, allCategoriesAndProducts = t, Supreme.categories = new Categories, Supreme.categories.populate(allCategoriesAndProducts), _.isUndefined(e) || (console.log("callback!"), e());
                if (!_.isUndefined(currentRoute()) && !_.isUndefined(currentRoute().route)) {
                    var s = currentRoute().route;
                    if (s == "categories") JSON.stringify(Supreme.categories).hashCode() != _currentViewHash.categories && Supreme.app.categories();
                    else if (s == "categoryProductList") {
                        var o = Supreme.categories.find(function(e) {
                            return e.get("name") == currentRoute().params[0]
                        });
                        _.isUndefined(o) ? Supreme.app.navigate("#", {
                            trigger: !0
                        }) : JSON.stringify(o.get("products")).hashCode() != _currentViewHash.categoryProductList && Supreme.app.categoryProductList(currentRoute().params[0])
                    }
                }
            }
        }
    })
}

function mixpanelTrack(e) {
    try {
        if (!mixpanel) return !1;
        mo = {}, mo["Event Name"] = e, mo.URL = location.pathname, mo.Season = SEASON_NO, mo["Release Date"] = window.release_date, mo["Release Week"] = window.release_week, mo["Page Name"] = document.title, mo["Device Type"] = "Mobile", obj = [].slice.call(arguments)[1], e == "Purchase Attempt" ? $.each(obj, function(e, t) {
            mixpanel.track("Purchase Attempt", $.extend(mo, t))
        }) : (obj && (mo = $.extend(mo, obj)), mixpanel.track(e, mo))
    } catch (t) {
        console.log(t)
    }
}

function clearCookies() {
    localStorage.clear(), eraseCookie("shoppingSessionId"), eraseCookie("_supreme_sess"), eraseCookie("cart"), eraseCookie("pure_cart"), eraseCookie("cart")
}(function() {
    "use strict";

    function e(t, r) {
        function s(e, t) {
            return function() {
                return e.apply(t, arguments)
            }
        }
        var i;
        r = r || {}, this.trackingClick = !1, this.trackingClickStart = 0, this.targetElement = null, this.touchStartX = 0, this.touchStartY = 0, this.lastTouchIdentifier = 0, this.touchBoundary = r.touchBoundary || 10, this.layer = t, this.tapDelay = r.tapDelay || 200, this.tapTimeout = r.tapTimeout || 700;
        if (e.notNeeded(t)) return;
        var o = ["onMouse", "onClick", "onTouchStart", "onTouchMove", "onTouchEnd", "onTouchCancel"],
            u = this;
        for (var a = 0, f = o.length; a < f; a++) u[o[a]] = s(u[o[a]], u);
        n && (t.addEventListener("mouseover", this.onMouse, !0), t.addEventListener("mousedown", this.onMouse, !0), t.addEventListener("mouseup", this.onMouse, !0)), t.addEventListener("click", this.onClick, !0), t.addEventListener("touchstart", this.onTouchStart, !1), t.addEventListener("touchmove", this.onTouchMove, !1), t.addEventListener("touchend", this.onTouchEnd, !1), t.addEventListener("touchcancel", this.onTouchCancel, !1), Event.prototype.stopImmediatePropagation || (t.removeEventListener = function(e, n, r) {
            var i = Node.prototype.removeEventListener;
            e === "click" ? i.call(t, e, n.hijacked || n, r) : i.call(t, e, n, r)
        }, t.addEventListener = function(e, n, r) {
            var i = Node.prototype.addEventListener;
            e === "click" ? i.call(t, e, n.hijacked || (n.hijacked = function(e) {
                e.propagationStopped || n(e)
            }), r) : i.call(t, e, n, r)
        }), typeof t.onclick == "function" && (i = t.onclick, t.addEventListener("click", function(e) {
            i(e)
        }, !1), t.onclick = null)
    }
    var t = navigator.userAgent.indexOf("Windows Phone") >= 0,
        n = navigator.userAgent.indexOf("Android") > 0 && !t,
        r = /iP(ad|hone|od)/.test(navigator.userAgent) && !t,
        i = r && /OS 4_\d(_\d)?/.test(navigator.userAgent),
        s = r && /OS [6-7]_\d/.test(navigator.userAgent),
        o = navigator.userAgent.indexOf("BB10") > 0;
    e.prototype.needsClick = function(e) {
        switch (e.nodeName.toLowerCase()) {
            case "button":
            case "select":
            case "textarea":
                if (e.disabled) return !0;
                break;
            case "input":
                if (r && e.type === "file" || e.disabled) return !0;
                break;
            case "label":
            case "iframe":
            case "video":
                return !0
        }
        return /\bneedsclick\b/.test(e.className)
    }, e.prototype.needsFocus = function(e) {
        switch (e.nodeName.toLowerCase()) {
            case "textarea":
                return !0;
            case "select":
                return !n;
            case "input":
                switch (e.type) {
                    case "button":
                    case "checkbox":
                    case "file":
                    case "image":
                    case "radio":
                    case "submit":
                        return !1
                }
                return !e.disabled && !e.readOnly;
            default:
                return /\bneedsfocus\b/.test(e.className)
        }
    }, e.prototype.sendClick = function(e, t) {
        var n, r;
        document.activeElement && document.activeElement !== e && document.activeElement.blur(), r = t.changedTouches[0], n = document.createEvent("MouseEvents"), n.initMouseEvent(this.determineEventType(e), !0, !0, window, 1, r.screenX, r.screenY, r.clientX, r.clientY, !1, !1, !1, !1, 0, null), n.forwardedTouchEvent = !0, e.dispatchEvent(n)
    }, e.prototype.determineEventType = function(e) {
        return n && e.tagName.toLowerCase() === "select" ? "mousedown" : "click"
    }, e.prototype.focus = function(e) {
        var t;
        r && e.setSelectionRange && e.type.indexOf("date") !== 0 && e.type !== "time" && e.type !== "month" ? (t = e.value.length, e.setSelectionRange(t, t)) : e.focus()
    }, e.prototype.updateScrollParent = function(e) {
        var t, n;
        t = e.fastClickScrollParent;
        if (!t || !t.contains(e)) {
            n = e;
            do {
                if (n.scrollHeight > n.offsetHeight) {
                    t = n, e.fastClickScrollParent = n;
                    break
                }
                n = n.parentElement
            } while (n)
        }
        t && (t.fastClickLastScrollTop = t.scrollTop)
    }, e.prototype.getTargetElementFromEventTarget = function(e) {
        return e.nodeType === Node.TEXT_NODE ? e.parentNode : e
    }, e.prototype.onTouchStart = function(e) {
        var t, n, s;
        if (e.targetTouches.length > 1) return !0;
        t = this.getTargetElementFromEventTarget(e.target), n = e.targetTouches[0];
        if (r) {
            s = window.getSelection();
            if (s.rangeCount && !s.isCollapsed) return !0;
            if (!i) {
                if (n.identifier && n.identifier === this.lastTouchIdentifier) return e.preventDefault(), !1;
                this.lastTouchIdentifier = n.identifier, this.updateScrollParent(t)
            }
        }
        return this.trackingClick = !0, this.trackingClickStart = e.timeStamp, this.targetElement = t, this.touchStartX = n.pageX, this.touchStartY = n.pageY, e.timeStamp - this.lastClickTime < this.tapDelay && e.preventDefault(), !0
    }, e.prototype.touchHasMoved = function(e) {
        var t = e.changedTouches[0],
            n = this.touchBoundary;
        return Math.abs(t.pageX - this.touchStartX) > n || Math.abs(t.pageY - this.touchStartY) > n ? !0 : !1
    }, e.prototype.onTouchMove = function(e) {
        if (!this.trackingClick) return !0;
        if (this.targetElement !== this.getTargetElementFromEventTarget(e.target) || this.touchHasMoved(e)) this.trackingClick = !1, this.targetElement = null;
        return !0
    }, e.prototype.findControl = function(e) {
        return e.control !== undefined ? e.control : e.htmlFor ? document.getElementById(e.htmlFor) : e.querySelector("button, input:not([type=hidden]), keygen, meter, output, progress, select, textarea")
    }, e.prototype.onTouchEnd = function(e) {
        var t, o, u, a, f, l = this.targetElement;
        if (!this.trackingClick) return !0;
        if (e.timeStamp - this.lastClickTime < this.tapDelay) return this.cancelNextClick = !0, !0;
        if (e.timeStamp - this.trackingClickStart > this.tapTimeout) return !0;
        this.cancelNextClick = !1, this.lastClickTime = e.timeStamp, o = this.trackingClickStart, this.trackingClick = !1, this.trackingClickStart = 0, s && (f = e.changedTouches[0], l = document.elementFromPoint(f.pageX - window.pageXOffset, f.pageY - window.pageYOffset) || l, l.fastClickScrollParent = this.targetElement.fastClickScrollParent), u = l.tagName.toLowerCase();
        if (u === "label") {
            t = this.findControl(l);
            if (t) {
                this.focus(l);
                if (n) return !1;
                l = t
            }
        } else if (this.needsFocus(l)) {
            if (e.timeStamp - o > 100 || r && window.top !== window && u === "input") return this.targetElement = null, !1;
            this.focus(l), this.sendClick(l, e);
            if (!r || u !== "select") this.targetElement = null, e.preventDefault();
            return !1
        }
        if (r && !i) {
            a = l.fastClickScrollParent;
            if (a && a.fastClickLastScrollTop !== a.scrollTop) return !0
        }
        return this.needsClick(l) || (e.preventDefault(), this.sendClick(l, e)), !1
    }, e.prototype.onTouchCancel = function() {
        this.trackingClick = !1, this.targetElement = null
    }, e.prototype.onMouse = function(e) {
        return this.targetElement ? e.forwardedTouchEvent ? !0 : e.cancelable ? !this.needsClick(this.targetElement) || this.cancelNextClick ? (e.stopImmediatePropagation ? e.stopImmediatePropagation() : e.propagationStopped = !0, e.stopPropagation(), e.preventDefault(), !1) : !0 : !0 : !0
    }, e.prototype.onClick = function(e) {
        var t;
        return this.trackingClick ? (this.targetElement = null, this.trackingClick = !1, !0) : e.target.type === "submit" && e.detail === 0 ? !0 : (t = this.onMouse(e), t || (this.targetElement = null), t)
    }, e.prototype.destroy = function() {
        var e = this.layer;
        n && (e.removeEventListener("mouseover", this.onMouse, !0), e.removeEventListener("mousedown", this.onMouse, !0), e.removeEventListener("mouseup", this.onMouse, !0)), e.removeEventListener("click", this.onClick, !0), e.removeEventListener("touchstart", this.onTouchStart, !1), e.removeEventListener("touchmove", this.onTouchMove, !1), e.removeEventListener("touchend", this.onTouchEnd, !1), e.removeEventListener("touchcancel", this.onTouchCancel, !1)
    }, e.notNeeded = function(e) {
        var t, r, i, s;
        if (typeof window.ontouchstart == "undefined") return !0;
        r = +(/Chrome\/([0-9]+)/.exec(navigator.userAgent) || [, 0])[1];
        if (r) {
            if (!n) return !0;
            t = document.querySelector("meta[name=viewport]");
            if (t) {
                if (t.content.indexOf("user-scalable=no") !== -1) return !0;
                if (r > 31 && document.documentElement.scrollWidth <= window.outerWidth) return !0
            }
        }
        if (o) {
            i = navigator.userAgent.match(/Version\/([0-9]*)\.([0-9]*)/);
            if (i[1] >= 10 && i[2] >= 3) {
                t = document.querySelector("meta[name=viewport]");
                if (t) {
                    if (t.content.indexOf("user-scalable=no") !== -1) return !0;
                    if (document.documentElement.scrollWidth <= window.outerWidth) return !0
                }
            }
        }
        if (e.style.msTouchAction === "none" || e.style.touchAction === "manipulation") return !0;
        s = +(/Firefox\/([0-9]+)/.exec(navigator.userAgent) || [, 0])[1];
        if (s >= 27) {
            t = document.querySelector("meta[name=viewport]");
            if (t && (t.content.indexOf("user-scalable=no") !== -1 || document.documentElement.scrollWidth <= window.outerWidth)) return !0
        }
        return e.style.touchAction === "none" || e.style.touchAction === "manipulation" ? !0 : !1
    }, e.attach = function(t, n) {
        return new e(t, n)
    }, typeof define == "function" && typeof define.amd == "object" && define.amd ? define(function() {
        return e
    }) : typeof module != "undefined" && module.exports ? (module.exports = e.attach, module.exports.FastClick = e) : window.FastClick = e
})(),
function(e, t) {
    typeof define == "function" && define.amd ? define(function() {
        return t(e)
    }) : t(e)
}(this, function(window) {
    var Zepto = function() {
        function _(e) {
            return e == null ? String(e) : T[N.call(e)] || "object"
        }

        function D(e) {
            return _(e) == "function"
        }

        function P(e) {
            return e != null && e == e.window
        }

        function H(e) {
            return e != null && e.nodeType == e.DOCUMENT_NODE
        }

        function B(e) {
            return _(e) == "object"
        }

        function j(e) {
            return B(e) && !P(e) && Object.getPrototypeOf(e) == Object.prototype
        }

        function F(e) {
            var t = !!e && "length" in e && e.length,
                r = n.type(e);
            return "function" != r && !P(e) && ("array" == r || t === 0 || typeof t == "number" && t > 0 && t - 1 in e)
        }

        function I(e) {
            return o.call(e, function(e) {
                return e != null
            })
        }

        function q(e) {
            return e.length > 0 ? n.fn.concat.apply([], e) : e
        }

        function R(e) {
            return e.replace(/::/g, "/").replace(/([A-Z]+)([A-Z][a-z])/g, "$1_$2").replace(/([a-z\d])([A-Z])/g, "$1_$2").replace(/_/g, "-").toLowerCase()
        }

        function U(e) {
            return e in l ? l[e] : l[e] = new RegExp("(^|\\s)" + e + "(\\s|$)")
        }

        function z(e, t) {
            return typeof t == "number" && !c[R(e)] ? t + "px" : t
        }

        function W(e) {
            var t, n;
            return f[e] || (t = a.createElement(e), a.body.appendChild(t), n = getComputedStyle(t, "").getPropertyValue("display"), t.parentNode.removeChild(t), n == "none" && (n = "block"), f[e] = n), f[e]
        }

        function X(e) {
            return "children" in e ? u.call(e.children) : n.map(e.childNodes, function(e) {
                if (e.nodeType == 1) return e
            })
        }

        function V(e, t) {
            var n, r = e ? e.length : 0;
            for (n = 0; n < r; n++) this[n] = e[n];
            this.length = r, this.selector = t || ""
        }

        function $(n, r, i) {
            for (t in r) i && (j(r[t]) || M(r[t])) ? (j(r[t]) && !j(n[t]) && (n[t] = {}), M(r[t]) && !M(n[t]) && (n[t] = []), $(n[t], r[t], i)) : r[t] !== e && (n[t] = r[t])
        }

        function J(e, t) {
            return t == null ? n(e) : n(e).filter(t)
        }

        function K(e, t, n, r) {
            return D(t) ? t.call(e, n, r) : t
        }

        function Q(e, t, n) {
            n == null ? e.removeAttribute(t) : e.setAttribute(t, n)
        }

        function G(t, n) {
            var r = t.className || "",
                i = r && r.baseVal !== e;
            if (n === e) return i ? r.baseVal : r;
            i ? r.baseVal = n : t.className = n
        }

        function Y(e) {
            try {
                return e ? e == "true" || (e == "false" ? !1 : e == "null" ? null : +e + "" == e ? +e : /^[\[\{]/.test(e) ? n.parseJSON(e) : e) : e
            } catch (t) {
                return e
            }
        }

        function Z(e, t) {
            t(e);
            for (var n = 0, r = e.childNodes.length; n < r; n++) Z(e.childNodes[n], t)
        }
        var e, t, n, r, i = [],
            s = i.concat,
            o = i.filter,
            u = i.slice,
            a = window.document,
            f = {},
            l = {},
            c = {
                "column-count": 1,
                columns: 1,
                "font-weight": 1,
                "line-height": 1,
                opacity: 1,
                "z-index": 1,
                zoom: 1
            },
            h = /^\s*<(\w+|!)[^>]*>/,
            p = /^<(\w+)\s*\/?>(?:<\/\1>|)$/,
            d = /<(?!area|br|col|embed|hr|img|input|link|meta|param)(([\w:]+)[^>]*)\/>/ig,
            v = /^(?:body|html)$/i,
            m = /([A-Z])/g,
            g = ["val", "css", "html", "text", "data", "width", "height", "offset"],
            y = ["after", "prepend", "before", "append"],
            b = a.createElement("table"),
            w = a.createElement("tr"),
            E = {
                tr: a.createElement("tbody"),
                tbody: b,
                thead: b,
                tfoot: b,
                td: w,
                th: w,
                "*": a.createElement("div")
            },
            S = /complete|loaded|interactive/,
            x = /^[\w-]*$/,
            T = {},
            N = T.toString,
            C = {},
            k, L, A = a.createElement("div"),
            O = {
                tabindex: "tabIndex",
                readonly: "readOnly",
                "for": "htmlFor",
                "class": "className",
                maxlength: "maxLength",
                cellspacing: "cellSpacing",
                cellpadding: "cellPadding",
                rowspan: "rowSpan",
                colspan: "colSpan",
                usemap: "useMap",
                frameborder: "frameBorder",
                contenteditable: "contentEditable"
            },
            M = Array.isArray || function(e) {
                return e instanceof Array
            };
        return C.matches = function(e, t) {
            if (!t || !e || e.nodeType !== 1) return !1;
            var n = e.matches || e.webkitMatchesSelector || e.mozMatchesSelector || e.oMatchesSelector || e.matchesSelector;
            if (n) return n.call(e, t);
            var r, i = e.parentNode,
                s = !i;
            return s && (i = A).appendChild(e), r = ~C.qsa(i, t).indexOf(e), s && A.removeChild(e), r
        }, k = function(e) {
            return e.replace(/-+(.)?/g, function(e, t) {
                return t ? t.toUpperCase() : ""
            })
        }, L = function(e) {
            return o.call(e, function(t, n) {
                return e.indexOf(t) == n
            })
        }, C.fragment = function(t, r, i) {
            var s, o, f;
            return p.test(t) && (s = n(a.createElement(RegExp.$1))), s || (t.replace && (t = t.replace(d, "<$1></$2>")), r === e && (r = h.test(t) && RegExp.$1), r in E || (r = "*"), f = E[r], f.innerHTML = "" + t, s = n.each(u.call(f.childNodes), function() {
                f.removeChild(this)
            })), j(i) && (o = n(s), n.each(i, function(e, t) {
                g.indexOf(e) > -1 ? o[e](t) : o.attr(e, t)
            })), s
        }, C.Z = function(e, t) {
            return new V(e, t)
        }, C.isZ = function(e) {
            return e instanceof C.Z
        }, C.init = function(t, r) {
            var i;
            if (!t) return C.Z();
            if (typeof t == "string") {
                t = t.trim();
                if (t[0] == "<" && h.test(t)) i = C.fragment(t, RegExp.$1, r), t = null;
                else {
                    if (r !== e) return n(r).find(t);
                    i = C.qsa(a, t)
                }
            } else {
                if (D(t)) return n(a).ready(t);
                if (C.isZ(t)) return t;
                if (M(t)) i = I(t);
                else if (B(t)) i = [t], t = null;
                else if (h.test(t)) i = C.fragment(t.trim(), RegExp.$1, r), t = null;
                else {
                    if (r !== e) return n(r).find(t);
                    i = C.qsa(a, t)
                }
            }
            return C.Z(i, t)
        }, n = function(e, t) {
            return C.init(e, t)
        }, n.extend = function(e) {
            var t, n = u.call(arguments, 1);
            return typeof e == "boolean" && (t = e, e = n.shift()), n.forEach(function(n) {
                $(e, n, t)
            }), e
        }, C.qsa = function(e, t) {
            var n, r = t[0] == "#",
                i = !r && t[0] == ".",
                s = r || i ? t.slice(1) : t,
                o = x.test(s);
            return e.getElementById && o && r ? (n = e.getElementById(s)) ? [n] : [] : e.nodeType !== 1 && e.nodeType !== 9 && e.nodeType !== 11 ? [] : u.call(o && !r && e.getElementsByClassName ? i ? e.getElementsByClassName(s) : e.getElementsByTagName(t) : e.querySelectorAll(t))
        }, n.contains = a.documentElement.contains ? function(e, t) {
            return e !== t && e.contains(t)
        } : function(e, t) {
            while (t && (t = t.parentNode))
                if (t === e) return !0;
            return !1
        }, n.type = _, n.isFunction = D, n.isWindow = P, n.isArray = M, n.isPlainObject = j, n.isEmptyObject = function(e) {
            var t;
            for (t in e) return !1;
            return !0
        }, n.isNumeric = function(e) {
            var t = Number(e),
                n = typeof e;
            return e != null && n != "boolean" && (n != "string" || e.length) && !isNaN(t) && isFinite(t) || !1
        }, n.inArray = function(e, t, n) {
            return i.indexOf.call(t, e, n)
        }, n.camelCase = k, n.trim = function(e) {
            return e == null ? "" : String.prototype.trim.call(e)
        }, n.uuid = 0, n.support = {}, n.expr = {}, n.noop = function() {}, n.map = function(e, t) {
            var n, r = [],
                i, s;
            if (F(e))
                for (i = 0; i < e.length; i++) n = t(e[i], i), n != null && r.push(n);
            else
                for (s in e) n = t(e[s], s), n != null && r.push(n);
            return q(r)
        }, n.each = function(e, t) {
            var n, r;
            if (F(e)) {
                for (n = 0; n < e.length; n++)
                    if (t.call(e[n], n, e[n]) === !1) return e
            } else
                for (r in e)
                    if (t.call(e[r], r, e[r]) === !1) return e;
            return e
        }, n.grep = function(e, t) {
            return o.call(e, t)
        }, window.JSON && (n.parseJSON = JSON.parse), n.each("Boolean Number String Function Array Date RegExp Object Error".split(" "), function(e, t) {
            T["[object " + t + "]"] = t.toLowerCase()
        }), n.fn = {
            constructor: C.Z,
            length: 0,
            forEach: i.forEach,
            reduce: i.reduce,
            push: i.push,
            sort: i.sort,
            splice: i.splice,
            indexOf: i.indexOf,
            concat: function() {
                var e, t, n = [];
                for (e = 0; e < arguments.length; e++) t = arguments[e], n[e] = C.isZ(t) ? t.toArray() : t;
                return s.apply(C.isZ(this) ? this.toArray() : this, n)
            },
            map: function(e) {
                return n(n.map(this, function(t, n) {
                    return e.call(t, n, t)
                }))
            },
            slice: function() {
                return n(u.apply(this, arguments))
            },
            ready: function(e) {
                return S.test(a.readyState) && a.body ? e(n) : a.addEventListener("DOMContentLoaded", function() {
                    e(n)
                }, !1), this
            },
            get: function(t) {
                return t === e ? u.call(this) : this[t >= 0 ? t : t + this.length]
            },
            toArray: function() {
                return this.get()
            },
            size: function() {
                return this.length
            },
            remove: function() {
                return this.each(function() {
                    this.parentNode != null && this.parentNode.removeChild(this)
                })
            },
            each: function(e) {
                return i.every.call(this, function(t, n) {
                    return e.call(t, n, t) !== !1
                }), this
            },
            filter: function(e) {
                return D(e) ? this.not(this.not(e)) : n(o.call(this, function(t) {
                    return C.matches(t, e)
                }))
            },
            add: function(e, t) {
                return n(L(this.concat(n(e, t))))
            },
            is: function(e) {
                return this.length > 0 && C.matches(this[0], e)
            },
            not: function(t) {
                var r = [];
                if (D(t) && t.call !== e) this.each(function(e) {
                    t.call(this, e) || r.push(this)
                });
                else {
                    var i = typeof t == "string" ? this.filter(t) : F(t) && D(t.item) ? u.call(t) : n(t);
                    this.forEach(function(e) {
                        i.indexOf(e) < 0 && r.push(e)
                    })
                }
                return n(r)
            },
            has: function(e) {
                return this.filter(function() {
                    return B(e) ? n.contains(this, e) : n(this).find(e).size()
                })
            },
            eq: function(e) {
                return e === -1 ? this.slice(e) : this.slice(e, +e + 1)
            },
            first: function() {
                var e = this[0];
                return e && !B(e) ? e : n(e)
            },
            last: function() {
                var e = this[this.length - 1];
                return e && !B(e) ? e : n(e)
            },
            find: function(e) {
                var t, r = this;
                return e ? typeof e == "object" ? t = n(e).filter(function() {
                    var e = this;
                    return i.some.call(r, function(t) {
                        return n.contains(t, e)
                    })
                }) : this.length == 1 ? t = n(C.qsa(this[0], e)) : t = this.map(function() {
                    return C.qsa(this, e)
                }) : t = n(), t
            },
            closest: function(e, t) {
                var r = [],
                    i = typeof e == "object" && n(e);
                return this.each(function(n, s) {
                    while (s && !(i ? i.indexOf(s) >= 0 : C.matches(s, e))) s = s !== t && !H(s) && s.parentNode;
                    s && r.indexOf(s) < 0 && r.push(s)
                }), n(r)
            },
            parents: function(e) {
                var t = [],
                    r = this;
                while (r.length > 0) r = n.map(r, function(e) {
                    if ((e = e.parentNode) && !H(e) && t.indexOf(e) < 0) return t.push(e), e
                });
                return J(t, e)
            },
            parent: function(e) {
                return J(L(this.pluck("parentNode")), e)
            },
            children: function(e) {
                return J(this.map(function() {
                    return X(this)
                }), e)
            },
            contents: function() {
                return this.map(function() {
                    return this.contentDocument || u.call(this.childNodes)
                })
            },
            siblings: function(e) {
                return J(this.map(function(e, t) {
                    return o.call(X(t.parentNode), function(e) {
                        return e !== t
                    })
                }), e)
            },
            empty: function() {
                return this.each(function() {
                    this.innerHTML = ""
                })
            },
            pluck: function(e) {
                return n.map(this, function(t) {
                    return t[e]
                })
            },
            show: function() {
                return this.each(function() {
                    this.style.display == "none" && (this.style.display = ""), getComputedStyle(this, "").getPropertyValue("display") == "none" && (this.style.display = W(this.nodeName))
                })
            },
            replaceWith: function(e) {
                return this.before(e).remove()
            },
            wrap: function(e) {
                var t = D(e);
                if (this[0] && !t) var r = n(e).get(0),
                    i = r.parentNode || this.length > 1;
                return this.each(function(s) {
                    n(this).wrapAll(t ? e.call(this, s) : i ? r.cloneNode(!0) : r)
                })
            },
            wrapAll: function(e) {
                if (this[0]) {
                    n(this[0]).before(e = n(e));
                    var t;
                    while ((t = e.children()).length) e = t.first();
                    n(e).append(this)
                }
                return this
            },
            wrapInner: function(e) {
                var t = D(e);
                return this.each(function(r) {
                    var i = n(this),
                        s = i.contents(),
                        o = t ? e.call(this, r) : e;
                    s.length ? s.wrapAll(o) : i.append(o)
                })
            },
            unwrap: function() {
                return this.parent().each(function() {
                    n(this).replaceWith(n(this).children())
                }), this
            },
            clone: function() {
                return this.map(function() {
                    return this.cloneNode(!0)
                })
            },
            hide: function() {
                return this.css("display", "none")
            },
            toggle: function(t) {
                return this.each(function() {
                    var r = n(this);
                    (t === e ? r.css("display") == "none" : t) ? r.show(): r.hide()
                })
            },
            prev: function(e) {
                return n(this.pluck("previousElementSibling")).filter(e || "*")
            },
            next: function(e) {
                return n(this.pluck("nextElementSibling")).filter(e || "*")
            },
            html: function(e) {
                return 0 in arguments ? this.each(function(t) {
                    var r = this.innerHTML;
                    n(this).empty().append(K(this, e, t, r))
                }) : 0 in this ? this[0].innerHTML : null
            },
            text: function(e) {
                return 0 in arguments ? this.each(function(t) {
                    var n = K(this, e, t, this.textContent);
                    this.textContent = n == null ? "" : "" + n
                }) : 0 in this ? this.pluck("textContent").join("") : null
            },
            attr: function(n, r) {
                var i;
                return typeof n != "string" || 1 in arguments ? this.each(function(e) {
                    if (this.nodeType !== 1) return;
                    if (B(n))
                        for (t in n) Q(this, t, n[t]);
                    else Q(this, n, K(this, r, e, this.getAttribute(n)))
                }) : 0 in this && this[0].nodeType == 1 && (i = this[0].getAttribute(n)) != null ? i : e
            },
            removeAttr: function(e) {
                return this.each(function() {
                    this.nodeType === 1 && e.split(" ").forEach(function(e) {
                        Q(this, e)
                    }, this)
                })
            },
            prop: function(e, t) {
                return e = O[e] || e, 1 in arguments ? this.each(function(n) {
                    this[e] = K(this, t, n, this[e])
                }) : this[0] && this[0][e]
            },
            removeProp: function(e) {
                return e = O[e] || e, this.each(function() {
                    delete this[e]
                })
            },
            data: function(t, n) {
                var r = "data-" + t.replace(m, "-$1").toLowerCase(),
                    i = 1 in arguments ? this.attr(r, n) : this.attr(r);
                return i !== null ? Y(i) : e
            },
            val: function(e) {
                return 0 in arguments ? (e == null && (e = ""), this.each(function(t) {
                    this.value = K(this, e, t, this.value)
                })) : this[0] && (this[0].multiple ? n(this[0]).find("option").filter(function() {
                    return this.selected
                }).pluck("value") : this[0].value)
            },
            offset: function(e) {
                if (e) return this.each(function(t) {
                    var r = n(this),
                        i = K(this, e, t, r.offset()),
                        s = r.offsetParent().offset(),
                        o = {
                            top: i.top - s.top,
                            left: i.left - s.left
                        };
                    r.css("position") == "static" && (o.position = "relative"), r.css(o)
                });
                if (!this.length) return null;
                if (a.documentElement !== this[0] && !n.contains(a.documentElement, this[0])) return {
                    top: 0,
                    left: 0
                };
                var t = this[0].getBoundingClientRect();
                return {
                    left: t.left + window.pageXOffset,
                    top: t.top + window.pageYOffset,
                    width: Math.round(t.width),
                    height: Math.round(t.height)
                }
            },
            css: function(e, r) {
                if (arguments.length < 2) {
                    var i = this[0];
                    if (typeof e == "string") {
                        if (!i) return;
                        return i.style[k(e)] || getComputedStyle(i, "").getPropertyValue(e)
                    }
                    if (M(e)) {
                        if (!i) return;
                        var s = {},
                            o = getComputedStyle(i, "");
                        return n.each(e, function(e, t) {
                            s[t] = i.style[k(t)] || o.getPropertyValue(t)
                        }), s
                    }
                }
                var u = "";
                if (_(e) == "string") !r && r !== 0 ? this.each(function() {
                    this.style.removeProperty(R(e))
                }) : u = R(e) + ":" + z(e, r);
                else
                    for (t in e) !e[t] && e[t] !== 0 ? this.each(function() {
                        this.style.removeProperty(R(t))
                    }) : u += R(t) + ":" + z(t, e[t]) + ";";
                return this.each(function() {
                    this.style.cssText += ";" + u
                })
            },
            index: function(e) {
                return e ? this.indexOf(n(e)[0]) : this.parent().children().indexOf(this[0])
            },
            hasClass: function(e) {
                return e ? i.some.call(this, function(e) {
                    return this.test(G(e))
                }, U(e)) : !1
            },
            addClass: function(e) {
                return e ? this.each(function(t) {
                    if (!("className" in this)) return;
                    r = [];
                    var i = G(this),
                        s = K(this, e, t, i);
                    s.split(/\s+/g).forEach(function(e) {
                        n(this).hasClass(e) || r.push(e)
                    }, this), r.length && G(this, i + (i ? " " : "") + r.join(" "))
                }) : this
            },
            removeClass: function(t) {
                return this.each(function(n) {
                    if (!("className" in this)) return;
                    if (t === e) return G(this, "");
                    r = G(this), K(this, t, n, r).split(/\s+/g).forEach(function(e) {
                        r = r.replace(U(e), " ")
                    }), G(this, r.trim())
                })
            },
            toggleClass: function(t, r) {
                return t ? this.each(function(i) {
                    var s = n(this),
                        o = K(this, t, i, G(this));
                    o.split(/\s+/g).forEach(function(t) {
                        (r === e ? !s.hasClass(t) : r) ? s.addClass(t): s.removeClass(t)
                    })
                }) : this
            },
            scrollTop: function(t) {
                if (!this.length) return;
                var n = "scrollTop" in this[0];
                return t === e ? n ? this[0].scrollTop : this[0].pageYOffset : this.each(n ? function() {
                    this.scrollTop = t
                } : function() {
                    this.scrollTo(this.scrollX, t)
                })
            },
            scrollLeft: function(t) {
                if (!this.length) return;
                var n = "scrollLeft" in this[0];
                return t === e ? n ? this[0].scrollLeft : this[0].pageXOffset : this.each(n ? function() {
                    this.scrollLeft = t
                } : function() {
                    this.scrollTo(t, this.scrollY)
                })
            },
            position: function() {
                if (!this.length) return;
                var e = this[0],
                    t = this.offsetParent(),
                    r = this.offset(),
                    i = v.test(t[0].nodeName) ? {
                        top: 0,
                        left: 0
                    } : t.offset();
                return r.top -= parseFloat(n(e).css("margin-top")) || 0, r.left -= parseFloat(n(e).css("margin-left")) || 0, i.top += parseFloat(n(t[0]).css("border-top-width")) || 0, i.left += parseFloat(n(t[0]).css("border-left-width")) || 0, {
                    top: r.top - i.top,
                    left: r.left - i.left
                }
            },
            offsetParent: function() {
                return this.map(function() {
                    var e = this.offsetParent || a.body;
                    while (e && !v.test(e.nodeName) && n(e).css("position") == "static") e = e.offsetParent;
                    return e
                })
            }
        }, n.fn.detach = n.fn.remove, ["width", "height"].forEach(function(t) {
            var r = t.replace(/./, function(e) {
                return e[0].toUpperCase()
            });
            n.fn[t] = function(i) {
                var s, o = this[0];
                return i === e ? P(o) ? o["inner" + r] : H(o) ? o.documentElement["scroll" + r] : (s = this.offset()) && s[t] : this.each(function(e) {
                    o = n(this), o.css(t, K(this, i, e, o[t]()))
                })
            }
        }), y.forEach(function(t, r) {
            var i = r % 2;
            n.fn[t] = function() {
                var t, s = n.map(arguments, function(r) {
                        var i = [];
                        return t = _(r), t == "array" ? (r.forEach(function(t) {
                            if (t.nodeType !== e) return i.push(t);
                            if (n.zepto.isZ(t)) return i = i.concat(t.get());
                            i = i.concat(C.fragment(t))
                        }), i) : t == "object" || r == null ? r : C.fragment(r)
                    }),
                    o, u = this.length > 1;
                return s.length < 1 ? this : this.each(function(e, t) {
                    o = i ? t : t.parentNode, t = r == 0 ? t.nextSibling : r == 1 ? t.firstChild : r == 2 ? t : null;
                    var f = n.contains(a.documentElement, o);
                    s.forEach(function(e) {
                        if (u) e = e.cloneNode(!0);
                        else if (!o) return n(e).remove();
                        o.insertBefore(e, t), f && Z(e, function(e) {
                            if (e.nodeName != null && e.nodeName.toUpperCase() === "SCRIPT" && (!e.type || e.type === "text/javascript") && !e.src) {
                                var t = e.ownerDocument ? e.ownerDocument.defaultView : window;
                                t.eval.call(t, e.innerHTML)
                            }
                        })
                    })
                })
            }, n.fn[i ? t + "To" : "insert" + (r ? "Before" : "After")] = function(e) {
                return n(e)[t](this), this
            }
        }), C.Z.prototype = V.prototype = n.fn, C.uniq = L, C.deserializeValue = Y, n.zepto = C, n
    }();
    return window.Zepto = Zepto, window.$ === undefined && (window.$ = Zepto),
        function(e) {
            function c(e) {
                return e._zid || (e._zid = t++)
            }

            function h(e, t, n, r) {
                t = p(t);
                if (t.ns) var i = d(t.ns);
                return (o[c(e)] || []).filter(function(e) {
                    return e && (!t.e || e.e == t.e) && (!t.ns || i.test(e.ns)) && (!n || c(e.fn) === c(n)) && (!r || e.sel == r)
                })
            }

            function p(e) {
                var t = ("" + e).split(".");
                return {
                    e: t[0],
                    ns: t.slice(1).sort().join(" ")
                }
            }

            function d(e) {
                return new RegExp("(?:^| )" + e.replace(" ", " .* ?") + "(?: |$)")
            }

            function v(e, t) {
                return e.del && !a && e.e in f || !!t
            }

            function m(e) {
                return l[e] || a && f[e] || e
            }

            function g(t, r, i, s, u, a, f) {
                var h = c(t),
                    d = o[h] || (o[h] = []);
                r.split(/\s/).forEach(function(r) {
                    if (r == "ready") return e(document).ready(i);
                    var o = p(r);
                    o.fn = i, o.sel = u, o.e in l && (i = function(t) {
                        var n = t.relatedTarget;
                        if (!n || n !== this && !e.contains(this, n)) return o.fn.apply(this, arguments)
                    }), o.del = a;
                    var c = a || i;
                    o.proxy = function(e) {
                        e = x(e);
                        if (e.isImmediatePropagationStopped()) return;
                        e.data = s;
                        var r = c.apply(t, e._args == n ? [e] : [e].concat(e._args));
                        return r === !1 && (e.preventDefault(), e.stopPropagation()), r
                    }, o.i = d.length, d.push(o), "addEventListener" in t && t.addEventListener(m(o.e), o.proxy, v(o, f))
                })
            }

            function y(e, t, n, r, i) {
                var s = c(e);
                (t || "").split(/\s/).forEach(function(t) {
                    h(e, t, n, r).forEach(function(t) {
                        delete o[s][t.i], "removeEventListener" in e && e.removeEventListener(m(t.e), t.proxy, v(t, i))
                    })
                })
            }

            function x(t, r) {
                if (r || !t.isDefaultPrevented) {
                    r || (r = t), e.each(S, function(e, n) {
                        var i = r[e];
                        t[e] = function() {
                            return this[n] = b, i && i.apply(r, arguments)
                        }, t[n] = w
                    }), t.timeStamp || (t.timeStamp = Date.now());
                    if (r.defaultPrevented !== n ? r.defaultPrevented : "returnValue" in r ? r.returnValue === !1 : r.getPreventDefault && r.getPreventDefault()) t.isDefaultPrevented = b
                }
                return t
            }

            function T(e) {
                var t, r = {
                    originalEvent: e
                };
                for (t in e) !E.test(t) && e[t] !== n && (r[t] = e[t]);
                return x(r, e)
            }
            var t = 1,
                n, r = Array.prototype.slice,
                i = e.isFunction,
                s = function(e) {
                    return typeof e == "string"
                },
                o = {},
                u = {},
                a = "onfocusin" in window,
                f = {
                    focus: "focusin",
                    blur: "focusout"
                },
                l = {
                    mouseenter: "mouseover",
                    mouseleave: "mouseout"
                };
            u.click = u.mousedown = u.mouseup = u.mousemove = "MouseEvents", e.event = {
                add: g,
                remove: y
            }, e.proxy = function(t, n) {
                var o = 2 in arguments && r.call(arguments, 2);
                if (i(t)) {
                    var u = function() {
                        return t.apply(n, o ? o.concat(r.call(arguments)) : arguments)
                    };
                    return u._zid = c(t), u
                }
                if (s(n)) return o ? (o.unshift(t[n], t), e.proxy.apply(null, o)) : e.proxy(t[n], t);
                throw new TypeError("expected function")
            }, e.fn.bind = function(e, t, n) {
                return this.on(e, t, n)
            }, e.fn.unbind = function(e, t) {
                return this.off(e, t)
            }, e.fn.one = function(e, t, n, r) {
                return this.on(e, t, n, r, 1)
            };
            var b = function() {
                    return !0
                },
                w = function() {
                    return !1
                },
                E = /^([A-Z]|returnValue$|layer[XY]$|webkitMovement[XY]$)/,
                S = {
                    preventDefault: "isDefaultPrevented",
                    stopImmediatePropagation: "isImmediatePropagationStopped",
                    stopPropagation: "isPropagationStopped"
                };
            e.fn.delegate = function(e, t, n) {
                return this.on(t, e, n)
            }, e.fn.undelegate = function(e, t, n) {
                return this.off(t, e, n)
            }, e.fn.live = function(t, n) {
                return e(document.body).delegate(this.selector, t, n), this
            }, e.fn.die = function(t, n) {
                return e(document.body).undelegate(this.selector, t, n), this
            }, e.fn.on = function(t, o, u, a, f) {
                var l, c, h = this;
                if (t && !s(t)) return e.each(t, function(e, t) {
                    h.on(e, o, u, t, f)
                }), h;
                !s(o) && !i(a) && a !== !1 && (a = u, u = o, o = n);
                if (a === n || u === !1) a = u, u = n;
                return a === !1 && (a = w), h.each(function(n, i) {
                    f && (l = function(e) {
                        return y(i, e.type, a), a.apply(this, arguments)
                    }), o && (c = function(t) {
                        var n, s = e(t.target).closest(o, i).get(0);
                        if (s && s !== i) return n = e.extend(T(t), {
                            currentTarget: s,
                            liveFired: i
                        }), (l || a).apply(s, [n].concat(r.call(arguments, 1)))
                    }), g(i, t, a, u, o, c || l)
                })
            }, e.fn.off = function(t, r, o) {
                var u = this;
                return t && !s(t) ? (e.each(t, function(e, t) {
                    u.off(e, r, t)
                }), u) : (!s(r) && !i(o) && o !== !1 && (o = r, r = n), o === !1 && (o = w), u.each(function() {
                    y(this, t, o, r)
                }))
            }, e.fn.trigger = function(t, n) {
                return t = s(t) || e.isPlainObject(t) ? e.Event(t) : x(t), t._args = n, this.each(function() {
                    t.type in f && typeof this[t.type] == "function" ? this[t.type]() : "dispatchEvent" in this ? this.dispatchEvent(t) : e(this).triggerHandler(t, n)
                })
            }, e.fn.triggerHandler = function(t, n) {
                var r, i;
                return this.each(function(o, u) {
                    r = T(s(t) ? e.Event(t) : t), r._args = n, r.target = u, e.each(h(u, t.type || t), function(e, t) {
                        i = t.proxy(r);
                        if (r.isImmediatePropagationStopped()) return !1
                    })
                }), i
            }, "focusin focusout focus blur load resize scroll unload click dblclick mousedown mouseup mousemove mouseover mouseout mouseenter mouseleave change select keydown keypress keyup error".split(" ").forEach(function(t) {
                e.fn[t] = function(e) {
                    return 0 in arguments ? this.bind(t, e) : this.trigger(t)
                }
            }), e.Event = function(e, t) {
                s(e) || (t = e, e = t.type);
                var n = document.createEvent(u[e] || "Events"),
                    r = !0;
                if (t)
                    for (var i in t) i == "bubbles" ? r = !!t[i] : n[i] = t[i];
                return n.initEvent(e, r, !0), x(n)
            }
        }(Zepto),
        function($) {
            function triggerAndReturn(e, t, n) {
                var r = $.Event(t);
                return $(e).trigger(r, n), !r.isDefaultPrevented()
            }

            function triggerGlobal(e, t, n, r) {
                if (e.global) return triggerAndReturn(t || document, n, r)
            }

            function ajaxStart(e) {
                e.global && $.active++ === 0 && triggerGlobal(e, null, "ajaxStart")
            }

            function ajaxStop(e) {
                e.global && !--$.active && triggerGlobal(e, null, "ajaxStop")
            }

            function ajaxBeforeSend(e, t) {
                var n = t.context;
                if (t.beforeSend.call(n, e, t) === !1 || triggerGlobal(t, n, "ajaxBeforeSend", [e, t]) === !1) return !1;
                triggerGlobal(t, n, "ajaxSend", [e, t])
            }

            function ajaxSuccess(e, t, n, r) {
                var i = n.context,
                    s = "success";
                n.success.call(i, e, s, t), r && r.resolveWith(i, [e, s, t]), triggerGlobal(n, i, "ajaxSuccess", [t, n, e]), ajaxComplete(s, t, n)
            }

            function ajaxError(e, t, n, r, i) {
                var s = r.context;
                r.error.call(s, n, t, e), i && i.rejectWith(s, [n, t, e]), triggerGlobal(r, s, "ajaxError", [n, r, e || t]), ajaxComplete(t, n, r)
            }

            function ajaxComplete(e, t, n) {
                var r = n.context;
                n.complete.call(r, t, e), triggerGlobal(n, r, "ajaxComplete", [t, n]), ajaxStop(n)
            }

            function ajaxDataFilter(e, t, n) {
                if (n.dataFilter == empty) return e;
                var r = n.context;
                return n.dataFilter.call(r, e, t)
            }

            function empty() {}

            function mimeToDataType(e) {
                return e && (e = e.split(";", 2)[0]), e && (e == htmlType ? "html" : e == jsonType ? "json" : scriptTypeRE.test(e) ? "script" : xmlTypeRE.test(e) && "xml") || "text"
            }

            function appendQuery(e, t) {
                return t == "" ? e : (e + "&" + t).replace(/[&?]{1,2}/, "?")
            }

            function serializeData(e) {
                e.processData && e.data && $.type(e.data) != "string" && (e.data = $.param(e.data, e.traditional)), e.data && (!e.type || e.type.toUpperCase() == "GET" || "jsonp" == e.dataType) && (e.url = appendQuery(e.url, e.data), e.data = undefined)
            }

            function parseArguments(e, t, n, r) {
                return $.isFunction(t) && (r = n, n = t, t = undefined), $.isFunction(n) || (r = n, n = undefined), {
                    url: e,
                    data: t,
                    success: n,
                    dataType: r
                }
            }

            function serialize(e, t, n, r) {
                var i, s = $.isArray(t),
                    o = $.isPlainObject(t);
                $.each(t, function(t, u) {
                    i = $.type(u), r && (t = n ? r : r + "[" + (o || i == "object" || i == "array" ? t : "") + "]"), !r && s ? e.add(u.name, u.value) : i == "array" || !n && i == "object" ? serialize(e, u, n, t) : e.add(t, u)
                })
            }
            var jsonpID = +(new Date),
                document = window.document,
                key, name, rscript = /<script\b[^<]*(?:(?!<\/script>)<[^<]*)*<\/script>/gi,
                scriptTypeRE = /^(?:text|application)\/javascript/i,
                xmlTypeRE = /^(?:text|application)\/xml/i,
                jsonType = "application/json",
                htmlType = "text/html",
                blankRE = /^\s*$/,
                originAnchor = document.createElement("a");
            originAnchor.href = window.location.href, $.active = 0, $.ajaxJSONP = function(e, t) {
                if ("type" in e) {
                    var n = e.jsonpCallback,
                        r = ($.isFunction(n) ? n() : n) || "Zepto" + jsonpID++,
                        i = document.createElement("script"),
                        s = window[r],
                        o, u = function(e) {
                            $(i).triggerHandler("error", e || "abort")
                        },
                        a = {
                            abort: u
                        },
                        f;
                    return t && t.promise(a), $(i).on("load error", function(n, u) {
                        clearTimeout(f), $(i).off().remove(), n.type == "error" || !o ? ajaxError(null, u || "error", a, e, t) : ajaxSuccess(o[0], a, e, t), window[r] = s, o && $.isFunction(s) && s(o[0]), s = o = undefined
                    }), ajaxBeforeSend(a, e) === !1 ? (u("abort"), a) : (window[r] = function() {
                        o = arguments
                    }, i.src = e.url.replace(/\?(.+)=\?/, "?$1=" + r), document.head.appendChild(i), e.timeout > 0 && (f = setTimeout(function() {
                        u("timeout")
                    }, e.timeout)), a)
                }
                return $.ajax(e)
            }, $.ajaxSettings = {
                type: "GET",
                beforeSend: empty,
                success: empty,
                error: empty,
                complete: empty,
                context: null,
                global: !0,
                xhr: function() {
                    return new window.XMLHttpRequest
                },
                accepts: {
                    script: "text/javascript, application/javascript, application/x-javascript",
                    json: jsonType,
                    xml: "application/xml, text/xml",
                    html: htmlType,
                    text: "text/plain"
                },
                crossDomain: !1,
                timeout: 0,
                processData: !0,
                cache: !0,
                dataFilter: empty
            }, $.ajax = function(options) {
                var settings = $.extend({}, options || {}),
                    deferred = $.Deferred && $.Deferred(),
                    urlAnchor, hashIndex;
                for (key in $.ajaxSettings) settings[key] === undefined && (settings[key] = $.ajaxSettings[key]);
                ajaxStart(settings), settings.crossDomain || (urlAnchor = document.createElement("a"), urlAnchor.href = settings.url, urlAnchor.href = urlAnchor.href, settings.crossDomain = originAnchor.protocol + "//" + originAnchor.host != urlAnchor.protocol + "//" + urlAnchor.host), settings.url || (settings.url = window.location.toString()), (hashIndex = settings.url.indexOf("#")) > -1 && (settings.url = settings.url.slice(0, hashIndex)), serializeData(settings);
                var dataType = settings.dataType,
                    hasPlaceholder = /\?.+=\?/.test(settings.url);
                hasPlaceholder && (dataType = "jsonp");
                if (settings.cache === !1 || (!options || options.cache !== !0) && ("script" == dataType || "jsonp" == dataType)) settings.url = appendQuery(settings.url, "_=" + Date.now());
                if ("jsonp" == dataType) return hasPlaceholder || (settings.url = appendQuery(settings.url, settings.jsonp ? settings.jsonp + "=?" : settings.jsonp === !1 ? "" : "callback=?")), $.ajaxJSONP(settings, deferred);
                var mime = settings.accepts[dataType],
                    headers = {},
                    setHeader = function(e, t) {
                        headers[e.toLowerCase()] = [e, t]
                    },
                    protocol = /^([\w-]+:)\/\//.test(settings.url) ? RegExp.$1 : window.location.protocol,
                    xhr = settings.xhr(),
                    nativeSetHeader = xhr.setRequestHeader,
                    abortTimeout;
                deferred && deferred.promise(xhr), settings.crossDomain || setHeader("X-Requested-With", "XMLHttpRequest"), setHeader("Accept", mime || "*/*");
                if (mime = settings.mimeType || mime) mime.indexOf(",") > -1 && (mime = mime.split(",", 2)[0]), xhr.overrideMimeType && xhr.overrideMimeType(mime);
                (settings.contentType || settings.contentType !== !1 && settings.data && settings.type.toUpperCase() != "GET") && setHeader("Content-Type", settings.contentType || "application/x-www-form-urlencoded");
                if (settings.headers)
                    for (name in settings.headers) setHeader(name, settings.headers[name]);
                xhr.setRequestHeader = setHeader, xhr.onreadystatechange = function() {
                    if (xhr.readyState == 4) {
                        xhr.onreadystatechange = empty, clearTimeout(abortTimeout);
                        var result, error = !1;
                        if (xhr.status >= 200 && xhr.status < 300 || xhr.status == 304 || xhr.status == 0 && protocol == "file:") {
                            dataType = dataType || mimeToDataType(settings.mimeType || xhr.getResponseHeader("content-type"));
                            if (xhr.responseType == "arraybuffer" || xhr.responseType == "blob") result = xhr.response;
                            else {
                                result = xhr.responseText;
                                try {
                                    result = ajaxDataFilter(result, dataType, settings), dataType == "script" ? (1, eval)(result) : dataType == "xml" ? result = xhr.responseXML : dataType == "json" && (result = blankRE.test(result) ? null : $.parseJSON(result))
                                } catch (e) {
                                    error = e
                                }
                                if (error) return ajaxError(error, "parsererror", xhr, settings, deferred)
                            }
                            ajaxSuccess(result, xhr, settings, deferred)
                        } else ajaxError(xhr.statusText || null, xhr.status ? "error" : "abort", xhr, settings, deferred)
                    }
                };
                if (ajaxBeforeSend(xhr, settings) === !1) return xhr.abort(), ajaxError(null, "abort", xhr, settings, deferred), xhr;
                var async = "async" in settings ? settings.async : !0;
                xhr.open(settings.type, settings.url, async, settings.username, settings.password);
                if (settings.xhrFields)
                    for (name in settings.xhrFields) xhr[name] = settings.xhrFields[name];
                for (name in headers) nativeSetHeader.apply(xhr, headers[name]);
                return settings.timeout > 0 && (abortTimeout = setTimeout(function() {
                    xhr.onreadystatechange = empty, xhr.abort(), ajaxError(null, "timeout", xhr, settings, deferred)
                }, settings.timeout)), xhr.send(settings.data ? settings.data : null), xhr
            }, $.get = function() {
                return $.ajax(parseArguments.apply(null, arguments))
            }, $.post = function() {
                var e = parseArguments.apply(null, arguments);
                return e.type = "POST", $.ajax(e)
            }, $.getJSON = function() {
                var e = parseArguments.apply(null, arguments);
                return e.dataType = "json", $.ajax(e)
            }, $.fn.load = function(e, t, n) {
                if (!this.length) return this;
                var r = this,
                    i = e.split(/\s/),
                    s, o = parseArguments(e, t, n),
                    u = o.success;
                return i.length > 1 && (o.url = i[0], s = i[1]), o.success = function(e) {
                    r.html(s ? $("<div>").html(e.replace(rscript, "")).find(s) : e), u && u.apply(r, arguments)
                }, $.ajax(o), this
            };
            var escape = encodeURIComponent;
            $.param = function(e, t) {
                var n = [];
                return n.add = function(e, t) {
                    $.isFunction(t) && (t = t()), t == null && (t = ""), this.push(escape(e) + "=" + escape(t))
                }, serialize(n, e, t), n.join("&").replace(/%20/g, "+")
            }
        }(Zepto),
        function(e) {
            e.fn.serializeArray = function() {
                var t, n, r = [],
                    i = function(e) {
                        if (e.forEach) return e.forEach(i);
                        r.push({
                            name: t,
                            value: e
                        })
                    };
                return this[0] && e.each(this[0].elements, function(r, s) {
                    n = s.type, t = s.name, t && s.nodeName.toLowerCase() != "fieldset" && !s.disabled && n != "submit" && n != "reset" && n != "button" && n != "file" && (n != "radio" && n != "checkbox" || s.checked) && i(e(s).val())
                }), r
            }, e.fn.serialize = function() {
                var e = [];
                return this.serializeArray().forEach(function(t) {
                    e.push(encodeURIComponent(t.name) + "=" + encodeURIComponent(t.value))
                }), e.join("&")
            }, e.fn.submit = function(t) {
                if (0 in arguments) this.bind("submit", t);
                else if (this.length) {
                    var n = e.Event("submit");
                    this.eq(0).trigger(n), n.isDefaultPrevented() || this.get(0).submit()
                }
                return this
            }
        }(Zepto),
        function() {
            try {
                getComputedStyle(undefined)
            } catch (e) {
                var t = getComputedStyle;
                window.getComputedStyle = function(e, n) {
                    try {
                        return t(e, n)
                    } catch (r) {
                        return null
                    }
                }
            }
        }(), Zepto
}),
function(e) {
    function o(s, o) {
        var a = s[i],
            f = a && t[a];
        if (o === undefined) return f || u(s);
        if (f) {
            if (o in f) return f[o];
            var l = r(o);
            if (l in f) return f[l]
        }
        return n.call(e(s), o)
    }

    function u(n, s, o) {
        var u = n[i] || (n[i] = ++e.uuid),
            f = t[u] || (t[u] = a(n));
        return s !== undefined && (f[r(s)] = o), f
    }

    function a(t) {
        var n = {};
        return e.each(t.attributes || s, function(t, i) {
            i.name.indexOf("data-") == 0 && (n[r(i.name.replace("data-", ""))] = e.zepto.deserializeValue(i.value))
        }), n
    }
    var t = {},
        n = e.fn.data,
        r = e.camelCase,
        i = e.expando = "Zepto" + +(new Date),
        s = [];
    e.fn.data = function(t, n) {
        return n === undefined ? e.isPlainObject(t) ? this.each(function(n, r) {
            e.each(t, function(e, t) {
                u(r, e, t)
            })
        }) : 0 in this ? o(this[0], t) : undefined : this.each(function() {
            u(this, t, n)
        })
    }, e.data = function(t, n, r) {
        return e(t).data(n, r)
    }, e.hasData = function(n) {
        var r = n[i],
            s = r && t[r];
        return s ? !e.isEmptyObject(s) : !1
    }, e.fn.removeData = function(n) {
        return typeof n == "string" && (n = n.split(/\s+/)), this.each(function() {
            var s = this[i],
                o = s && t[s];
            o && e.each(n || o, function(e) {
                delete o[n ? r(this) : e]
            })
        })
    }, ["remove", "empty"].forEach(function(t) {
        var n = e.fn[t];
        e.fn[t] = function() {
            var e = this.find("*");
            return t === "remove" && (e = e.add(this)), e.removeData(), n.call(this)
        }
    })
}(Zepto),
function(e, t) {
    function g(e) {
        return e.replace(/([A-Z])/g, "-$1").toLowerCase()
    }

    function y(e) {
        return r ? r + e : e.toLowerCase()
    }
    var n = "",
        r, i = {
            Webkit: "webkit",
            Moz: "",
            O: "o"
        },
        s = document.createElement("div"),
        o = /^((translate|rotate|scale)(X|Y|Z|3d)?|matrix(3d)?|perspective|skew(X|Y)?)$/i,
        u, a, f, l, c, h, p, d, v, m = {};
    s.style.transform === t && e.each(i, function(e, i) {
        if (s.style[e + "TransitionProperty"] !== t) return n = "-" + e.toLowerCase() + "-", r = i, !1
    }), u = n + "transform", m[a = n + "transition-property"] = m[f = n + "transition-duration"] = m[c = n + "transition-delay"] = m[l = n + "transition-timing-function"] = m[h = n + "animation-name"] = m[p = n + "animation-duration"] = m[v = n + "animation-delay"] = m[d = n + "animation-timing-function"] = "", e.fx = {
        off: r === t && s.style.transitionProperty === t,
        speeds: {
            _default: 400,
            fast: 200,
            slow: 600
        },
        cssPrefix: n,
        transitionEnd: y("TransitionEnd"),
        animationEnd: y("AnimationEnd")
    }, e.fn.animate = function(n, r, i, s, o) {
        return e.isFunction(r) && (s = r, i = t, r = t), e.isFunction(i) && (s = i, i = t), e.isPlainObject(r) && (i = r.easing, s = r.complete, o = r.delay, r = r.duration), r && (r = (typeof r == "number" ? r : e.fx.speeds[r] || e.fx.speeds._default) / 1e3), o && (o = parseFloat(o) / 1e3), this.anim(n, r, i, s, o)
    }, e.fn.anim = function(n, r, i, s, y) {
        var b, w = {},
            E, S = "",
            x = this,
            T, N = e.fx.transitionEnd,
            C = !1;
        r === t && (r = e.fx.speeds._default / 1e3), y === t && (y = 0), e.fx.off && (r = 0);
        if (typeof n == "string") w[h] = n, w[p] = r + "s", w[v] = y + "s", w[d] = i || "linear", N = e.fx.animationEnd;
        else {
            E = [];
            for (b in n) o.test(b) ? S += b + "(" + n[b] + ") " : (w[b] = n[b], E.push(g(b)));
            S && (w[u] = S, E.push(u)), r > 0 && typeof n == "object" && (w[a] = E.join(", "), w[f] = r + "s", w[c] = y + "s", w[l] = i || "linear")
        }
        return T = function(t) {
            if (typeof t != "undefined") {
                if (t.target !== t.currentTarget) return;
                e(t.target).unbind(N, T)
            } else e(this).unbind(N, T);
            C = !0, e(this).css(m), s && s.call(this)
        }, r > 0 && (this.bind(N, T), setTimeout(function() {
            if (C) return;
            T.call(x)
        }, (r + y) * 1e3 + 25)), this.size() && this.get(0).clientLeft, this.css(w), r <= 0 && setTimeout(function() {
            x.each(function() {
                T.call(this)
            })
        }, 0), this
    }, s = null
}(Zepto),
function(e) {
    function i(t) {
        return t = e(t), (!!t.width() || !!t.height()) && t.css("display") !== "none"
    }

    function f(e, t) {
        e = e.replace(/=#\]/g, '="#"]');
        var n, r, i = o.exec(e);
        if (i && i[2] in s) {
            n = s[i[2]], r = i[3], e = i[1];
            if (r) {
                var u = Number(r);
                isNaN(u) ? r = r.replace(/^["']|["']$/g, "") : r = u
            }
        }
        return t(e, n, r)
    }
    var t = e.zepto,
        n = t.qsa,
        r = t.matches,
        s = e.expr[":"] = {
            visible: function() {
                if (i(this)) return this
            },
            hidden: function() {
                if (!i(this)) return this
            },
            selected: function() {
                if (this.selected) return this
            },
            checked: function() {
                if (this.checked) return this
            },
            parent: function() {
                return this.parentNode
            },
            first: function(e) {
                if (e === 0) return this
            },
            last: function(e, t) {
                if (e === t.length - 1) return this
            },
            eq: function(e, t, n) {
                if (e === n) return this
            },
            contains: function(t, n, r) {
                if (e(this).text().indexOf(r) > -1) return this
            },
            has: function(e, n, r) {
                if (t.qsa(this, r).length) return this
            }
        },
        o = new RegExp("(.*):(\\w+)(?:\\(([^)]+)\\))?$\\s*"),
        u = /^\s*>/,
        a = "Zepto" + +(new Date);
    t.qsa = function(r, i) {
        return f(i, function(s, o, f) {
            try {
                var l;
                !s && o ? s = "*" : u.test(s) && (l = e(r).addClass(a), s = "." + a + " " + s);
                var c = n(r, s)
            } catch (h) {
                throw console.error("error performing selector: %o", i), h
            } finally {
                l && l.removeClass(a)
            }
            return o ? t.uniq(e.map(c, function(e, t) {
                return o.call(e, t, c, f)
            })) : c
        })
    }, t.matches = function(e, t) {
        return f(t, function(t, n, i) {
            return (!t || r(e, t)) && (!n || n.call(e, null, i) === e)
        })
    }
}(Zepto),
function(e) {
    var t = function(e, t, n) {
            return e + (t - e) * n
        },
        n = function(e) {
            return -Math.cos(e * Math.PI) / 2 + .5
        },
        r = function(r, i, s) {
            r = typeof r != "undefined" ? r : e.os.android ? 1 : 0, i = typeof i != "undefined" ? i : 200;
            if (i === 0) {
                window.scrollTo(0, r), typeof s == "function" && s();
                return
            }
            var o = window.pageYOffset,
                u = Date.now(),
                a = u + i,
                f = function() {
                    var e = Date.now(),
                        l = e > a ? 1 : (e - u) / i;
                    window.scrollTo(0, t(o, r, n(l))), e < a ? setTimeout(f, 15) : typeof s == "function" && s()
                };
            f()
        },
        i = function(e, r, i) {
            e = typeof e != "undefined" ? e : 0, r = typeof r != "undefined" ? r : 200;
            if (r === 0) {
                this.scrollTop = e, typeof i == "function" && i();
                return
            }
            var s = this.scrollTop,
                o = Date.now(),
                u = o + r,
                a = this,
                f = function() {
                    var l = Date.now(),
                        c = l > u ? 1 : (l - o) / r;
                    a.scrollTop = t(s, e, n(c)), l < u ? setTimeout(f, 15) : typeof i == "function" && i()
                };
            f()
        };
    e.scrollTo = r, e.fn.scrollTo = function() {
        if (this.length) {
            var e = arguments;
            this.forEach(function(t, n) {
                i.apply(t, e)
            })
        }
    }
}(Zepto),
function() {
    var e = this,
        t = e._,
        n = {},
        r = Array.prototype,
        i = Object.prototype,
        s = Function.prototype,
        o = r.push,
        u = r.slice,
        a = r.concat,
        f = i.toString,
        l = i.hasOwnProperty,
        c = r.forEach,
        h = r.map,
        p = r.reduce,
        d = r.reduceRight,
        v = r.filter,
        m = r.every,
        g = r.some,
        y = r.indexOf,
        b = r.lastIndexOf,
        w = Array.isArray,
        E = Object.keys,
        S = s.bind,
        x = function(e) {
            return e instanceof x ? e : this instanceof x ? (this._wrapped = e, void 0) : new x(e)
        };
    "undefined" != typeof exports ? ("undefined" != typeof module && module.exports && (exports = module.exports = x), exports._ = x) : e._ = x, x.VERSION = "1.5.2";
    var T = x.each = x.forEach = function(e, t, r) {
        if (null != e)
            if (c && e.forEach === c) e.forEach(t, r);
            else if (e.length === +e.length) {
            for (var i = 0, s = e.length; s > i; i++)
                if (t.call(r, e[i], i, e) === n) return
        } else
            for (var o = x.keys(e), i = 0, s = o.length; s > i; i++)
                if (t.call(r, e[o[i]], o[i], e) === n) return
    };
    x.map = x.collect = function(e, t, n) {
        var r = [];
        return null == e ? r : h && e.map === h ? e.map(t, n) : (T(e, function(e, i, s) {
            r.push(t.call(n, e, i, s))
        }), r)
    };
    var N = "Reduce of empty array with no initial value";
    x.reduce = x.foldl = x.inject = function(e, t, n, r) {
        var i = arguments.length > 2;
        if (null == e && (e = []), p && e.reduce === p) return r && (t = x.bind(t, r)), i ? e.reduce(t, n) : e.reduce(t);
        if (T(e, function(e, s, o) {
                i ? n = t.call(r, n, e, s, o) : (n = e, i = !0)
            }), !i) throw new TypeError(N);
        return n
    }, x.reduceRight = x.foldr = function(e, t, n, r) {
        var i = arguments.length > 2;
        if (null == e && (e = []), d && e.reduceRight === d) return r && (t = x.bind(t, r)), i ? e.reduceRight(t, n) : e.reduceRight(t);
        var s = e.length;
        if (s !== +s) {
            var o = x.keys(e);
            s = o.length
        }
        if (T(e, function(u, a, f) {
                a = o ? o[--s] : --s, i ? n = t.call(r, n, e[a], a, f) : (n = e[a], i = !0)
            }), !i) throw new TypeError(N);
        return n
    }, x.find = x.detect = function(e, t, n) {
        var r;
        return C(e, function(e, i, s) {
            return t.call(n, e, i, s) ? (r = e, !0) : void 0
        }), r
    }, x.filter = x.select = function(e, t, n) {
        var r = [];
        return null == e ? r : v && e.filter === v ? e.filter(t, n) : (T(e, function(e, i, s) {
            t.call(n, e, i, s) && r.push(e)
        }), r)
    }, x.reject = function(e, t, n) {
        return x.filter(e, function(e, r, i) {
            return !t.call(n, e, r, i)
        }, n)
    }, x.every = x.all = function(e, t, r) {
        t || (t = x.identity);
        var i = !0;
        return null == e ? i : m && e.every === m ? e.every(t, r) : (T(e, function(e, s, o) {
            return (i = i && t.call(r, e, s, o)) ? void 0 : n
        }), !!i)
    };
    var C = x.some = x.any = function(e, t, r) {
        t || (t = x.identity);
        var i = !1;
        return null == e ? i : g && e.some === g ? e.some(t, r) : (T(e, function(e, s, o) {
            return i || (i = t.call(r, e, s, o)) ? n : void 0
        }), !!i)
    };
    x.contains = x.include = function(e, t) {
        return null == e ? !1 : y && e.indexOf === y ? e.indexOf(t) != -1 : C(e, function(e) {
            return e === t
        })
    }, x.invoke = function(e, t) {
        var n = u.call(arguments, 2),
            r = x.isFunction(t);
        return x.map(e, function(e) {
            return (r ? t : e[t]).apply(e, n)
        })
    }, x.pluck = function(e, t) {
        return x.map(e, function(e) {
            return e[t]
        })
    }, x.where = function(e, t, n) {
        return x.isEmpty(t) ? n ? void 0 : [] : x[n ? "find" : "filter"](e, function(e) {
            for (var n in t)
                if (t[n] !== e[n]) return !1;
            return !0
        })
    }, x.findWhere = function(e, t) {
        return x.where(e, t, !0)
    }, x.max = function(e, t, n) {
        if (!t && x.isArray(e) && e[0] === +e[0] && e.length < 65535) return Math.max.apply(Math, e);
        if (!t && x.isEmpty(e)) return -1 / 0;
        var r = {
            computed: -1 / 0,
            value: -1 / 0
        };
        return T(e, function(e, i, s) {
            var o = t ? t.call(n, e, i, s) : e;
            o > r.computed && (r = {
                value: e,
                computed: o
            })
        }), r.value
    }, x.min = function(e, t, n) {
        if (!t && x.isArray(e) && e[0] === +e[0] && e.length < 65535) return Math.min.apply(Math, e);
        if (!t && x.isEmpty(e)) return 1 / 0;
        var r = {
            computed: 1 / 0,
            value: 1 / 0
        };
        return T(e, function(e, i, s) {
            var o = t ? t.call(n, e, i, s) : e;
            o < r.computed && (r = {
                value: e,
                computed: o
            })
        }), r.value
    }, x.shuffle = function(e) {
        var t, n = 0,
            r = [];
        return T(e, function(e) {
            t = x.random(n++), r[n - 1] = r[t], r[t] = e
        }), r
    }, x.sample = function(e, t, n) {
        return arguments.length < 2 || n ? e[x.random(e.length - 1)] : x.shuffle(e).slice(0, Math.max(0, t))
    };
    var k = function(e) {
        return x.isFunction(e) ? e : function(t) {
            return t[e]
        }
    };
    x.sortBy = function(e, t, n) {
        var r = k(t);
        return x.pluck(x.map(e, function(e, t, i) {
            return {
                value: e,
                index: t,
                criteria: r.call(n, e, t, i)
            }
        }).sort(function(e, t) {
            var n = e.criteria,
                r = t.criteria;
            if (n !== r) {
                if (n > r || n === void 0) return 1;
                if (r > n || r === void 0) return -1
            }
            return e.index - t.index
        }), "value")
    };
    var L = function(e) {
        return function(t, n, r) {
            var i = {},
                s = null == n ? x.identity : k(n);
            return T(t, function(n, o) {
                var u = s.call(r, n, o, t);
                e(i, u, n)
            }), i
        }
    };
    x.groupBy = L(function(e, t, n) {
        (x.has(e, t) ? e[t] : e[t] = []).push(n)
    }), x.indexBy = L(function(e, t, n) {
        e[t] = n
    }), x.countBy = L(function(e, t) {
        x.has(e, t) ? e[t]++ : e[t] = 1
    }), x.sortedIndex = function(e, t, n, r) {
        n = null == n ? x.identity : k(n);
        for (var i = n.call(r, t), s = 0, o = e.length; o > s;) {
            var u = s + o >>> 1;
            n.call(r, e[u]) < i ? s = u + 1 : o = u
        }
        return s
    }, x.toArray = function(e) {
        return e ? x.isArray(e) ? u.call(e) : e.length === +e.length ? x.map(e, x.identity) : x.values(e) : []
    }, x.size = function(e) {
        return null == e ? 0 : e.length === +e.length ? e.length : x.keys(e).length
    }, x.first = x.head = x.take = function(e, t, n) {
        return null == e ? void 0 : null == t || n ? e[0] : u.call(e, 0, t)
    }, x.initial = function(e, t, n) {
        return u.call(e, 0, e.length - (null == t || n ? 1 : t))
    }, x.last = function(e, t, n) {
        return null == e ? void 0 : null == t || n ? e[e.length - 1] : u.call(e, Math.max(e.length - t, 0))
    }, x.rest = x.tail = x.drop = function(e, t, n) {
        return u.call(e, null == t || n ? 1 : t)
    }, x.compact = function(e) {
        return x.filter(e, x.identity)
    };
    var A = function(e, t, n) {
        return t && x.every(e, x.isArray) ? a.apply(n, e) : (T(e, function(e) {
            x.isArray(e) || x.isArguments(e) ? t ? o.apply(n, e) : A(e, t, n) : n.push(e)
        }), n)
    };
    x.flatten = function(e, t) {
        return A(e, t, [])
    }, x.without = function(e) {
        return x.difference(e, u.call(arguments, 1))
    }, x.uniq = x.unique = function(e, t, n, r) {
        x.isFunction(t) && (r = n, n = t, t = !1);
        var i = n ? x.map(e, n, r) : e,
            s = [],
            o = [];
        return T(i, function(n, r) {
            (t ? r && o[o.length - 1] === n : x.contains(o, n)) || (o.push(n), s.push(e[r]))
        }), s
    }, x.union = function() {
        return x.uniq(x.flatten(arguments, !0))
    }, x.intersection = function(e) {
        var t = u.call(arguments, 1);
        return x.filter(x.uniq(e), function(e) {
            return x.every(t, function(t) {
                return x.indexOf(t, e) >= 0
            })
        })
    }, x.difference = function(e) {
        var t = a.apply(r, u.call(arguments, 1));
        return x.filter(e, function(e) {
            return !x.contains(t, e)
        })
    }, x.zip = function() {
        for (var e = x.max(x.pluck(arguments, "length").concat(0)), t = new Array(e), n = 0; e > n; n++) t[n] = x.pluck(arguments, "" + n);
        return t
    }, x.object = function(e, t) {
        if (null == e) return {};
        for (var n = {}, r = 0, i = e.length; i > r; r++) t ? n[e[r]] = t[r] : n[e[r][0]] = e[r][1];
        return n
    }, x.indexOf = function(e, t, n) {
        if (null == e) return -1;
        var r = 0,
            i = e.length;
        if (n) {
            if ("number" != typeof n) return r = x.sortedIndex(e, t), e[r] === t ? r : -1;
            r = 0 > n ? Math.max(0, i + n) : n
        }
        if (y && e.indexOf === y) return e.indexOf(t, n);
        for (; i > r; r++)
            if (e[r] === t) return r;
        return -1
    }, x.lastIndexOf = function(e, t, n) {
        if (null == e) return -1;
        var r = null != n;
        if (b && e.lastIndexOf === b) return r ? e.lastIndexOf(t, n) : e.lastIndexOf(t);
        for (var i = r ? n : e.length; i--;)
            if (e[i] === t) return i;
        return -1
    }, x.range = function(e, t, n) {
        arguments.length <= 1 && (t = e || 0, e = 0), n = arguments[2] || 1;
        for (var r = Math.max(Math.ceil((t - e) / n), 0), i = 0, s = new Array(r); r > i;) s[i++] = e, e += n;
        return s
    };
    var O = function() {};
    x.bind = function(e, t) {
        var n, r;
        if (S && e.bind === S) return S.apply(e, u.call(arguments, 1));
        if (!x.isFunction(e)) throw new TypeError;
        return n = u.call(arguments, 2), r = function() {
            if (this instanceof r) {
                O.prototype = e.prototype;
                var i = new O;
                O.prototype = null;
                var s = e.apply(i, n.concat(u.call(arguments)));
                return Object(s) === s ? s : i
            }
            return e.apply(t, n.concat(u.call(arguments)))
        }
    }, x.partial = function(e) {
        var t = u.call(arguments, 1);
        return function() {
            return e.apply(this, t.concat(u.call(arguments)))
        }
    }, x.bindAll = function(e) {
        var t = u.call(arguments, 1);
        if (0 === t.length) throw new Error("bindAll must be passed function names");
        return T(t, function(t) {
            e[t] = x.bind(e[t], e)
        }), e
    }, x.memoize = function(e, t) {
        var n = {};
        return t || (t = x.identity),
            function() {
                var r = t.apply(this, arguments);
                return x.has(n, r) ? n[r] : n[r] = e.apply(this, arguments)
            }
    }, x.delay = function(e, t) {
        var n = u.call(arguments, 2);
        return setTimeout(function() {
            return e.apply(null, n)
        }, t)
    }, x.defer = function(e) {
        return x.delay.apply(x, [e, 1].concat(u.call(arguments, 1)))
    }, x.throttle = function(e, t, n) {
        var r, i, s, o = null,
            u = 0;
        n || (n = {});
        var a = function() {
            u = n.leading === !1 ? 0 : new Date, o = null, s = e.apply(r, i)
        };
        return function() {
            var f = new Date;
            u || n.leading !== !1 || (u = f);
            var l = t - (f - u);
            return r = this, i = arguments, 0 >= l ? (clearTimeout(o), o = null, u = f, s = e.apply(r, i)) : o || n.trailing === !1 || (o = setTimeout(a, l)), s
        }
    }, x.debounce = function(e, t, n) {
        var r, i, s, o, u;
        return function() {
            s = this, i = arguments, o = new Date;
            var a = function() {
                    var f = new Date - o;
                    t > f ? r = setTimeout(a, t - f) : (r = null, n || (u = e.apply(
                        s, i)))
                },
                f = n && !r;
            return r || (r = setTimeout(a, t)), f && (u = e.apply(s, i)), u
        }
    }, x.once = function(e) {
        var t, n = !1;
        return function() {
            return n ? t : (n = !0, t = e.apply(this, arguments), e = null, t)
        }
    }, x.wrap = function(e, t) {
        return function() {
            var n = [e];
            return o.apply(n, arguments), t.apply(this, n)
        }
    }, x.compose = function() {
        var e = arguments;
        return function() {
            for (var t = arguments, n = e.length - 1; n >= 0; n--) t = [e[n].apply(this, t)];
            return t[0]
        }
    }, x.after = function(e, t) {
        return function() {
            return --e < 1 ? t.apply(this, arguments) : void 0
        }
    }, x.keys = E || function(e) {
        if (e !== Object(e)) throw new TypeError("Invalid object");
        var t = [];
        for (var n in e) x.has(e, n) && t.push(n);
        return t
    }, x.values = function(e) {
        for (var t = x.keys(e), n = t.length, r = new Array(n), i = 0; n > i; i++) r[i] = e[t[i]];
        return r
    }, x.pairs = function(e) {
        for (var t = x.keys(e), n = t.length, r = new Array(n), i = 0; n > i; i++) r[i] = [t[i], e[t[i]]];
        return r
    }, x.invert = function(e) {
        for (var t = {}, n = x.keys(e), r = 0, i = n.length; i > r; r++) t[e[n[r]]] = n[r];
        return t
    }, x.functions = x.methods = function(e) {
        var t = [];
        for (var n in e) x.isFunction(e[n]) && t.push(n);
        return t.sort()
    }, x.extend = function(e) {
        return T(u.call(arguments, 1), function(t) {
            if (t)
                for (var n in t) e[n] = t[n]
        }), e
    }, x.pick = function(e) {
        var t = {},
            n = a.apply(r, u.call(arguments, 1));
        return T(n, function(n) {
            n in e && (t[n] = e[n])
        }), t
    }, x.omit = function(e) {
        var t = {},
            n = a.apply(r, u.call(arguments, 1));
        for (var i in e) x.contains(n, i) || (t[i] = e[i]);
        return t
    }, x.defaults = function(e) {
        return T(u.call(arguments, 1), function(t) {
            if (t)
                for (var n in t) e[n] === void 0 && (e[n] = t[n])
        }), e
    }, x.clone = function(e) {
        return x.isObject(e) ? x.isArray(e) ? e.slice() : x.extend({}, e) : e
    }, x.tap = function(e, t) {
        return t(e), e
    };
    var M = function(e, t, n, r) {
        if (e === t) return 0 !== e || 1 / e == 1 / t;
        if (null == e || null == t) return e === t;
        e instanceof x && (e = e._wrapped), t instanceof x && (t = t._wrapped);
        var i = f.call(e);
        if (i != f.call(t)) return !1;
        switch (i) {
            case "[object String]":
                return e == String(t);
            case "[object Number]":
                return e != +e ? t != +t : 0 == e ? 1 / e == 1 / t : e == +t;
            case "[object Date]":
            case "[object Boolean]":
                return +e == +t;
            case "[object RegExp]":
                return e.source == t.source && e.global == t.global && e.multiline == t.multiline && e.ignoreCase == t.ignoreCase
        }
        if ("object" != typeof e || "object" != typeof t) return !1;
        for (var s = n.length; s--;)
            if (n[s] == e) return r[s] == t;
        var o = e.constructor,
            u = t.constructor;
        if (o !== u && !(x.isFunction(o) && o instanceof o && x.isFunction(u) && u instanceof u)) return !1;
        n.push(e), r.push(t);
        var a = 0,
            l = !0;
        if ("[object Array]" == i) {
            if (a = e.length, l = a == t.length)
                for (; a-- && (l = M(e[a], t[a], n, r)););
        } else {
            for (var c in e)
                if (x.has(e, c) && (a++, !(l = x.has(t, c) && M(e[c], t[c], n, r)))) break;
            if (l) {
                for (c in t)
                    if (x.has(t, c) && !(a--)) break;
                l = !a
            }
        }
        return n.pop(), r.pop(), l
    };
    x.isEqual = function(e, t) {
        return M(e, t, [], [])
    }, x.isEmpty = function(e) {
        if (null == e) return !0;
        if (x.isArray(e) || x.isString(e)) return 0 === e.length;
        for (var t in e)
            if (x.has(e, t)) return !1;
        return !0
    }, x.isElement = function(e) {
        return !!e && 1 === e.nodeType
    }, x.isArray = w || function(e) {
        return "[object Array]" == f.call(e)
    }, x.isObject = function(e) {
        return e === Object(e)
    }, T(["Arguments", "Function", "String", "Number", "Date", "RegExp"], function(e) {
        x["is" + e] = function(t) {
            return f.call(t) == "[object " + e + "]"
        }
    }), x.isArguments(arguments) || (x.isArguments = function(e) {
        return !!e && !!x.has(e, "callee")
    }), "function" != typeof /./ && (x.isFunction = function(e) {
        return "function" == typeof e
    }), x.isFinite = function(e) {
        return isFinite(e) && !isNaN(parseFloat(e))
    }, x.isNaN = function(e) {
        return x.isNumber(e) && e != +e
    }, x.isBoolean = function(e) {
        return e === !0 || e === !1 || "[object Boolean]" == f.call(e)
    }, x.isNull = function(e) {
        return null === e
    }, x.isUndefined = function(e) {
        return e === void 0
    }, x.has = function(e, t) {
        return l.call(e, t)
    }, x.noConflict = function() {
        return e._ = t, this
    }, x.identity = function(e) {
        return e
    }, x.times = function(e, t, n) {
        for (var r = Array(Math.max(0, e)), i = 0; e > i; i++) r[i] = t.call(n, i);
        return r
    }, x.random = function(e, t) {
        return null == t && (t = e, e = 0), e + Math.floor(Math.random() * (t - e + 1))
    };
    var _ = {
        escape: {
            "&": "&amp;",
            "<": "&lt;",
            ">": "&gt;",
            '"': "&quot;",
            "'": "&#x27;"
        }
    };
    _.unescape = x.invert(_.escape);
    var D = {
        escape: new RegExp("[" + x.keys(_.escape).join("") + "]", "g"),
        unescape: new RegExp("(" + x.keys(_.unescape).join("|") + ")", "g")
    };
    x.each(["escape", "unescape"], function(e) {
        x[e] = function(t) {
            return null == t ? "" : ("" + t).replace(D[e], function(t) {
                return _[e][t]
            })
        }
    }), x.result = function(e, t) {
        if (null == e) return void 0;
        var n = e[t];
        return x.isFunction(n) ? n.call(e) : n
    }, x.mixin = function(e) {
        T(x.functions(e), function(t) {
            var n = x[t] = e[t];
            x.prototype[t] = function() {
                var e = [this._wrapped];
                return o.apply(e, arguments), F.call(this, n.apply(x, e))
            }
        })
    };
    var P = 0;
    x.uniqueId = function(e) {
        var t = ++P + "";
        return e ? e + t : t
    }, x.templateSettings = {
        evaluate: /<%([\s\S]+?)%>/g,
        interpolate: /<%=([\s\S]+?)%>/g,
        escape: /<%-([\s\S]+?)%>/g
    };
    var H = /(.)^/,
        B = {
            "'": "'",
            "\\": "\\",
            "\r": "r",
            "\n": "n",
            "	": "t",
            "\u2028": "u2028",
            "\u2029": "u2029"
        },
        j = /\\|'|\r|\n|\t|\u2028|\u2029/g;
    x.template = function(e, t, n) {
        var r;
        n = x.defaults({}, n, x.templateSettings);
        var i = new RegExp([(n.escape || H).source, (n.interpolate || H).source, (n.evaluate || H).source].join("|") + "|$", "g"),
            s = 0,
            o = "__p+='";
        e.replace(i, function(t, n, r, i, u) {
            return o += e.slice(s, u).replace(j, function(e) {
                return "\\" + B[e]
            }), n && (o += "'+\n((__t=(" + n + "))==null?'':_.escape(__t))+\n'"), r && (o += "'+\n((__t=(" + r + "))==null?'':__t)+\n'"), i && (o += "';\n" + i + "\n__p+='"), s = u + t.length, t
        }), o += "';\n", n.variable || (o = "with(obj||{}){\n" + o + "}\n"), o = "var __t,__p='',__j=Array.prototype.join,print=function(){__p+=__j.call(arguments,'');};\n" + o + "return __p;\n";
        try {
            r = new Function(n.variable || "obj", "_", o)
        } catch (u) {
            throw u.source = o, u
        }
        if (t) return r(t, x);
        var a = function(e) {
            return r.call(this, e, x)
        };
        return a.source = "function(" + (n.variable || "obj") + "){\n" + o + "}", a
    }, x.chain = function(e) {
        return x(e).chain()
    };
    var F = function(e) {
        return this._chain ? x(e).chain() : e
    };
    x.mixin(x), T(["pop", "push", "reverse", "shift", "sort", "splice", "unshift"], function(e) {
        var t = r[e];
        x.prototype[e] = function() {
            var n = this._wrapped;
            return t.apply(n, arguments), "shift" != e && "splice" != e || 0 !== n.length || delete n[0], F.call(this, n)
        }
    }), T(["concat", "join", "slice"], function(e) {
        var t = r[e];
        x.prototype[e] = function() {
            return F.call(this, t.apply(this._wrapped, arguments))
        }
    }), x.extend(x.prototype, {
        chain: function() {
            return this._chain = !0, this
        },
        value: function() {
            return this._wrapped
        }
    })
}.call(this),
    function() {
        var e = this,
            t = e.Backbone,
            n = Array.prototype.slice,
            r = Array.prototype.splice,
            i;
        i = "undefined" != typeof exports ? exports : e.Backbone = {}, i.VERSION = "0.9.1";
        var s = e._;
        !s && "undefined" != typeof require && (s = require("underscore"));
        var o = e.jQuery || e.Zepto || e.ender;
        i.setDomLibrary = function(e) {
            o = e
        }, i.noConflict = function() {
            return e.Backbone = t, this
        }, i.emulateHTTP = !1, i.emulateJSON = !1, i.Events = {
            on: function(e, t, n) {
                for (var r, e = e.split(/\s+/), i = this._callbacks || (this._callbacks = {}); r = e.shift();) {
                    r = i[r] || (i[r] = {});
                    var s = r.tail || (r.tail = r.next = {});
                    s.callback = t, s.context = n, r.tail = s.next = {}
                }
                return this
            },
            off: function(e, t, n) {
                var r, i, s;
                if (e) {
                    if (i = this._callbacks)
                        for (e = e.split(/\s+/); r = e.shift();)
                            if (s = i[r], delete i[r], t && s)
                                for (;
                                    (s = s.next) && s.next;)(s.callback !== t || !!n && s.context !== n) && this.on(r, s.callback, s.context)
                } else delete this._callbacks;
                return this
            },
            trigger: function(e) {
                var t, r, i, s;
                if (!(i = this._callbacks)) return this;
                s = i.all;
                for ((e = e.split(/\s+/)).push(null); t = e.shift();) s && e.push({
                    next: s.next,
                    tail: s.tail,
                    event: t
                }), (r = i[t]) && e.push({
                    next: r.next,
                    tail: r.tail
                });
                for (s = n.call(arguments, 1); r = e.pop();) {
                    t = r.tail;
                    for (i = r.event ? [r.event].concat(s) : s;
                        (r = r.next) !== t;) r.callback.apply(r.context || this, i)
                }
                return this
            }
        }, i.Events.bind = i.Events.on, i.Events.unbind = i.Events.off, i.Model = function(e, t) {
            var n;
            e || (e = {}), t && t.parse && (e = this.parse(e));
            if (n = y(this, "defaults")) e = s.extend({}, n, e);
            t && t.collection && (this.collection = t.collection), this.attributes = {}, this._escapedAttributes = {}, this.cid = s.uniqueId("c");
            if (!this.set(e, {
                    silent: !0
                })) throw Error("Can't create an invalid model");
            delete this._changed, this._previousAttributes = s.clone(this.attributes), this.initialize.apply(this, arguments)
        }, s.extend(i.Model.prototype, i.Events, {
            idAttribute: "id",
            initialize: function() {},
            toJSON: function() {
                return s.clone(this.attributes)
            },
            get: function(e) {
                return this.attributes[e]
            },
            escape: function(e) {
                var t;
                return (t = this._escapedAttributes[e]) ? t : (t = this.attributes[e], this._escapedAttributes[e] = s.escape(null == t ? "" : "" + t))
            },
            has: function(e) {
                return null != this.attributes[e]
            },
            set: function(e, t, n) {
                var r, o;
                s.isObject(e) || null == e ? (r = e, n = t) : (r = {}, r[e] = t), n || (n = {});
                if (!r) return this;
                r instanceof i.Model && (r = r.attributes);
                if (n.unset)
                    for (o in r) r[o] = void 0;
                if (!this._validate(r, n)) return !1;
                this.idAttribute in r && (this.id = r[this.idAttribute]);
                var t = this.attributes,
                    u = this._escapedAttributes,
                    a = this._previousAttributes || {},
                    f = this._setting;
                this._changed || (this._changed = {}), this._setting = !0;
                for (o in r)
                    if (e = r[o], s.isEqual(t[o], e) || delete u[o], n.unset ? delete t[o] : t[o] = e, this._changing && !s.isEqual(this._changed[o], e) && (this.trigger("change:" + o, this, e, n), this._moreChanges = !0), delete this._changed[o], !s.isEqual(a[o], e) || s.has(t, o) != s.has(a, o)) this._changed[o] = e;
                return f || (!n.silent && this.hasChanged() && this.change(n), this._setting = !1), this
            },
            unset: function(e, t) {
                return (t || (t = {})).unset = !0, this.set(e, null, t)
            },
            clear: function(e) {
                return (e || (e = {})).unset = !0, this.set(s.clone(this.attributes), e)
            },
            fetch: function(e) {
                var e = e ? s.clone(e) : {},
                    t = this,
                    n = e.success;
                return e.success = function(r, i, s) {
                    if (!t.set(t.parse(r, s), e)) return !1;
                    n && n(t, r)
                }, e.error = i.wrapError(e.error, t, e), (this.sync || i.sync).call(this, "read", this, e)
            },
            save: function(e, t, n) {
                var r, o;
                s.isObject(e) || null == e ? (r = e, n = t) : (r = {}, r[e] = t), n = n ? s.clone(n) : {}, n.wait && (o = s.clone(this.attributes)), e = s.extend({}, n, {
                    silent: !0
                });
                if (r && !this.set(r, n.wait ? e : n)) return !1;
                var u = this,
                    a = n.success;
                return n.success = function(e, t, i) {
                    t = u.parse(e, i), n.wait && (t = s.extend(r || {}, t));
                    if (!u.set(t, n)) return !1;
                    a ? a(u, e) : u.trigger("sync", u, e, n)
                }, n.error = i.wrapError(n.error, u, n), t = this.isNew() ? "create" : "update", t = (this.sync || i.sync).call(this, t, this, n), n.wait && this.set(o, e), t
            },
            destroy: function(e) {
                var e = e ? s.clone(e) : {},
                    t = this,
                    n = e.success,
                    r = function() {
                        t.trigger("destroy", t, t.collection, e)
                    };
                if (this.isNew()) return r();
                e.success = function(i) {
                    e.wait && r(), n ? n(t, i) : t.trigger("sync", t, i, e)
                }, e.error = i.wrapError(e.error, t, e);
                var o = (this.sync || i.sync).call(this, "delete", this, e);
                return e.wait || r(), o
            },
            url: function() {
                var e = y(this.collection, "url") || y(this, "urlRoot") || b();
                return this.isNew() ? e : e + ("/" == e.charAt(e.length - 1) ? "" : "/") + encodeURIComponent(this.id)
            },
            parse: function(e) {
                return e
            },
            clone: function() {
                return new this.constructor(this.attributes)
            },
            isNew: function() {
                return null == this.id
            },
            change: function(e) {
                if (this._changing || !this.hasChanged()) return this;
                this._moreChanges = this._changing = !0;
                for (var t in this._changed) this.trigger("change:" + t, this, this._changed[t], e);
                for (; this._moreChanges;) this._moreChanges = !1, this.trigger("change", this, e);
                return this._previousAttributes = s.clone(this.attributes), delete this._changed, this._changing = !1, this
            },
            hasChanged: function(e) {
                return arguments.length ? this._changed && s.has(this._changed, e) : !s.isEmpty(this._changed)
            },
            changedAttributes: function(e) {
                if (!e) return this.hasChanged() ? s.clone(this._changed) : !1;
                var t, n = !1,
                    r = this._previousAttributes,
                    i;
                for (i in e) s.isEqual(r[i], t = e[i]) || ((n || (n = {}))[i] = t);
                return n
            },
            previous: function(e) {
                return !arguments.length || !this._previousAttributes ? null : this._previousAttributes[e]
            },
            previousAttributes: function() {
                return s.clone(this._previousAttributes)
            },
            isValid: function() {
                return !this.validate(this.attributes)
            },
            _validate: function(e, t) {
                if (t.silent || !this.validate) return !0;
                var e = s.extend({}, this.attributes, e),
                    n = this.validate(e, t);
                return n ? (t && t.error ? t.error(this, n, t) : this.trigger("error", this, n, t), !1) : !0
            }
        }), i.Collection = function(e, t) {
            t || (t = {}), t.comparator && (this.comparator = t.comparator), this._reset(), this.initialize.apply(this, arguments), e && this.reset(e, {
                silent: !0,
                parse: t.parse
            })
        }, s.extend(i.Collection.prototype, i.Events, {
            model: i.Model,
            initialize: function() {},
            toJSON: function() {
                return this.map(function(e) {
                    return e.toJSON()
                })
            },
            add: function(e, t) {
                var n, i, o, u, a, f = {},
                    l = {};
                t || (t = {}), e = s.isArray(e) ? e.slice() : [e];
                for (n = 0, i = e.length; n < i; n++) {
                    if (!(o = e[n] = this._prepareModel(e[n], t))) throw Error("Can't add an invalid model to a collection");
                    if (f[u = o.cid] || this._byCid[u] || null != (a = o.id) && (l[a] || this._byId[a])) throw Error("Can't add the same model to a collection twice");
                    f[u] = l[a] = o
                }
                for (n = 0; n < i; n++)(o = e[n]).on("all", this._onModelEvent, this), this._byCid[o.cid] = o, null != o.id && (this._byId[o.id] = o);
                this.length += i, r.apply(this.models, [null != t.at ? t.at : this.models.length, 0].concat(e)), this.comparator && this.sort({
                    silent: !0
                });
                if (t.silent) return this;
                for (n = 0, i = this.models.length; n < i; n++) f[(o = this.models[n]).cid] && (t.index = n, o.trigger("add", o, this, t));
                return this
            },
            remove: function(e, t) {
                var n, r, i, o;
                t || (t = {}), e = s.isArray(e) ? e.slice() : [e];
                for (n = 0, r = e.length; n < r; n++)
                    if (o = this.getByCid(e[n]) || this.get(e[n])) delete this._byId[o.id], delete this._byCid[o.cid], i = this.indexOf(o), this.models.splice(i, 1), this.length--, t.silent || (t.index = i, o.trigger("remove", o, this, t)), this._removeReference(o);
                return this
            },
            get: function(e) {
                return null == e ? null : this._byId[null != e.id ? e.id : e]
            },
            getByCid: function(e) {
                return e && this._byCid[e.cid || e]
            },
            at: function(e) {
                return this.models[e]
            },
            sort: function(e) {
                e || (e = {});
                if (!this.comparator) throw Error("Cannot sort a set without a comparator");
                var t = s.bind(this.comparator, this);
                return 1 == this.comparator.length ? this.models = this.sortBy(t) : this.models.sort(t), e.silent || this.trigger("reset", this, e), this
            },
            pluck: function(e) {
                return s.map(this.models, function(t) {
                    return t.get(e)
                })
            },
            reset: function(e, t) {
                e || (e = []), t || (t = {});
                for (var n = 0, r = this.models.length; n < r; n++) this._removeReference(this.models[n]);
                return this._reset(), this.add(e, {
                    silent: !0,
                    parse: t.parse
                }), t.silent || this.trigger("reset", this, t), this
            },
            fetch: function(e) {
                e = e ? s.clone(e) : {}, void 0 === e.parse && (e.parse = !0);
                var t = this,
                    n = e.success;
                return e.success = function(r, i, s) {
                    t[e.add ? "add" : "reset"](t.parse(r, s), e), n && n(t, r)
                }, e.error = i.wrapError(e.error, t, e), (this.sync || i.sync).call(this, "read", this, e)
            },
            create: function(e, t) {
                var n = this,
                    t = t ? s.clone(t) : {},
                    e = this._prepareModel(e, t);
                if (!e) return !1;
                t.wait || n.add(e, t);
                var r = t.success;
                return t.success = function(i, s) {
                    t.wait && n.add(i, t), r ? r(i, s) : i.trigger("sync", e, s, t)
                }, e.save(null, t), e
            },
            parse: function(e) {
                return e
            },
            chain: function() {
                return s(this.models).chain()
            },
            _reset: function() {
                this.length = 0, this.models = [], this._byId = {}, this._byCid = {}
            },
            _prepareModel: function(e, t) {
                return e instanceof i.Model ? e.collection || (e.collection = this) : (t.collection = this, e = new this.model(e, t), e._validate(e.attributes, t) || (e = !1)), e
            },
            _removeReference: function(e) {
                this == e.collection && delete e.collection, e.off("all", this._onModelEvent, this)
            },
            _onModelEvent: function(e, t, n, r) {
                ("add" == e || "remove" == e) && n != this || ("destroy" == e && this.remove(t, r), t && e === "change:" + t.idAttribute && (delete this._byId[t.previous(t.idAttribute)], this._byId[t.id] = t), this.trigger.apply(this, arguments))
            }
        }), s.each("forEach,each,map,reduce,reduceRight,find,detect,filter,select,reject,every,all,some,any,include,contains,invoke,max,min,sortBy,sortedIndex,toArray,size,first,initial,rest,last,without,indexOf,shuffle,lastIndexOf,isEmpty,groupBy".split(","), function(e) {
            i.Collection.prototype[e] = function() {
                return s[e].apply(s, [this.models].concat(s.toArray(arguments)))
            }
        }), i.Router = function(e) {
            e || (e = {}), e.routes && (this.routes = e.routes), this._bindRoutes(), this.initialize.apply(this, arguments)
        };
        var u = /:\w+/g,
            a = /\*\w+/g,
            f = /[-[\]{}()+?.,\\^$|#\s]/g;
        s.extend(i.Router.prototype, i.Events, {
            initialize: function() {},
            route: function(e, t, n) {
                return i.history || (i.history = new i.History), s.isRegExp(e) || (e = this._routeToRegExp(e)), n || (n = this[t]), i.history.route(e, s.bind(function(r) {
                    r = this._extractParameters(e, r), n && n.apply(this, r), this.trigger.apply(this, ["route:" + t].concat(r)), i.history.trigger("route", this, t, r)
                }, this)), this
            },
            navigate: function(e, t) {
                i.history.navigate(e, t)
            },
            _bindRoutes: function() {
                if (this.routes) {
                    var e = [],
                        t;
                    for (t in this.routes) e.unshift([t, this.routes[t]]);
                    t = 0;
                    for (var n = e.length; t < n; t++) this.route(e[t][0], e[t][1], this[e[t][1]])
                }
            },
            _routeToRegExp: function(e) {
                return e = e.replace(f, "\\$&").replace(u, "([^/]+)").replace(a, "(.*?)"), RegExp("^" + e + "$")
            },
            _extractParameters: function(e, t) {
                return e.exec(t).slice(1)
            }
        }), i.History = function() {
            this.handlers = [], s.bindAll(this, "checkUrl")
        };
        var l = /^[#\/]/,
            c = /msie [\w.]+/,
            h = !1;
        s.extend(i.History.prototype, i.Events, {
            interval: 50,
            getFragment: function(e, t) {
                if (null == e)
                    if (this._hasPushState || t) {
                        var e = window.location.pathname,
                            n = window.location.search;
                        n && (e += n)
                    } else e = window.location.hash;
                return e = decodeURIComponent(e), e.indexOf(this.options.root) || (e = e.substr(this.options.root.length)), e.replace(l, "")
            },
            start: function(e) {
                if (h) throw Error("Backbone.history has already been started");
                this.options = s.extend({}, {
                    root: "/"
                }, this.options, e), this._wantsHashChange = !1 !== this.options.hashChange, this._wantsPushState = !!this.options.pushState, this._hasPushState = !(!this.options.pushState || !window.history || !window.history.pushState);
                var e = this.getFragment(),
                    t = document.documentMode;
                if (t = c.exec(navigator.userAgent.toLowerCase()) && (!t || 7 >= t)) this.iframe = o('<iframe src="javascript:0" tabindex="-1" />').hide().appendTo("body")[0].contentWindow, this.navigate(e);
                this._hasPushState ? o(window).bind("popstate", this.checkUrl) : this._wantsHashChange && "onhashchange" in window && !t ? o(window).bind("hashchange", this.checkUrl) : this._wantsHashChange && (this._checkUrlInterval = setInterval(this.checkUrl, this.interval)), this.fragment = e, h = !0, e = window.location, t = e.pathname == this.options.root;
                if (this._wantsHashChange && this._wantsPushState && !this._hasPushState && !t) return this.fragment = this.getFragment(null, !0), window.location.replace(this.options.root + "#" + this.fragment), !0;
                this._wantsPushState && this._hasPushState && t && e.hash && (this.fragment = e.hash.replace(l, ""), window.history.replaceState({}, document.title, e.protocol + "//" + e.host + this.options.root + this.fragment));
                if (!this.options.silent) return this.loadUrl()
            },
            stop: function() {
                o(window).unbind("popstate", this.checkUrl).unbind("hashchange", this.checkUrl), clearInterval(this._checkUrlInterval), h = !1
            },
            route: function(e, t) {
                this.handlers.unshift({
                    route: e,
                    callback: t
                })
            },
            checkUrl: function() {
                var e = this.getFragment();
                e == this.fragment && this.iframe && (e = this.getFragment(this.iframe.location.hash));
                if (e == this.fragment || e == decodeURIComponent(this.fragment)) return !1;
                this.iframe && this.navigate(e), this.loadUrl() || this.loadUrl(window.location.hash)
            },
            loadUrl: function(e) {
                var t = this.fragment = this.getFragment(e);
                return s.any(this.handlers, function(e) {
                    if (e.route.test(t)) return e.callback(t), !0
                })
            },
            navigate: function(e, t) {
                if (!h) return !1;
                if (!t || !0 === t) t = {
                    trigger: t
                };
                var n = (e || "").replace(l, "");
                this.fragment == n || this.fragment == decodeURIComponent(n) || (this._hasPushState ? (0 != n.indexOf(this.options.root) && (n = this.options.root + n), this.fragment = n, window.history[t.replace ? "replaceState" : "pushState"]({}, document.title, n)) : this._wantsHashChange ? (this.fragment = n, this._updateHash(window.location, n, t.replace), this.iframe && n != this.getFragment(this.iframe.location.hash) && (t.replace || this.iframe.document.open().close(), this._updateHash(this.iframe.location, n, t.replace))) : window.location.assign(this.options.root + e), t.trigger && this.loadUrl(e))
            },
            _updateHash: function(e, t, n) {
                n ? e.replace(e.toString().replace(/(javascript:|#).*$/, "") + "#" + t) : e.hash = t
            }
        }), i.View = function(e) {
            this.cid = s.uniqueId("view"), this._configure(e || {}), this._ensureElement(), this.initialize.apply(this, arguments), this.delegateEvents()
        };
        var p = /^(\S+)\s*(.*)$/,
            d = "model,collection,el,id,attributes,className,tagName".split(",");
        s.extend(i.View.prototype, i.Events, {
            tagName: "div",
            $: function(e) {
                return this.$el.find(e)
            },
            initialize: function() {},
            render: function() {
                return this
            },
            remove: function() {
                return this.$el.remove(), this
            },
            make: function(e, t, n) {
                return e = document.createElement(e), t && o(e).attr(t), n && o(e).html(n), e
            },
            setElement: function(e, t) {
                return this.$el = o(e), this.el = this.$el[0], !1 !== t && this.delegateEvents(), this
            },
            delegateEvents: function(e) {
                if (e || (e = y(this, "events"))) {
                    this.undelegateEvents();
                    for (var t in e) {
                        var n = e[t];
                        s.isFunction(n) || (n = this[e[t]]);
                        if (!n) throw Error('Event "' + e[t] + '" does not exist');
                        var r = t.match(p),
                            i = r[1],
                            r = r[2],
                            n = s.bind(n, this),
                            i = i + (".delegateEvents" + this.cid);
                        "" === r ? this.$el.bind(i, n) : this.$el.delegate(r, i, n)
                    }
                }
            },
            undelegateEvents: function() {
                this.$el.unbind(".delegateEvents" + this.cid)
            },
            _configure: function(e) {
                this.options && (e = s.extend({}, this.options, e));
                for (var t = 0, n = d.length; t < n; t++) {
                    var r = d[t];
                    e[r] && (this[r] = e[r])
                }
                this.options = e
            },
            _ensureElement: function() {
                if (this.el) this.setElement(this.el, !1);
                else {
                    var e = y(this, "attributes") || {};
                    this.id && (e.id = this.id), this.className && (e["class"] = this.className), this.setElement(this.make(this.tagName, e), !1)
                }
            }
        }), i.Model.extend = i.Collection.extend = i.Router.extend = i.View.extend = function(e, t) {
            var n = g(this, e, t);
            return n.extend = this.extend, n
        };
        var v = {
            create: "POST",
            update: "PUT",
            "delete": "DELETE",
            read: "GET"
        };
        i.sync = function(e, t, n) {
            var r = v[e],
                u = {
                    type: r,
                    dataType: "json"
                };
            return n.url || (u.url = y(t, "url") || b()), !n.data && t && ("create" == e || "update" == e) && (u.contentType = "application/json", u.data = JSON.stringify(t.toJSON())), i.emulateJSON && (u.contentType = "application/x-www-form-urlencoded", u.data = u.data ? {
                model: u.data
            } : {}), i.emulateHTTP && ("PUT" === r || "DELETE" === r) && (i.emulateJSON && (u.data._method = r), u.type = "POST", u.beforeSend = function(e) {
                e.setRequestHeader("X-HTTP-Method-Override", r)
            }), "GET" !== u.type && !i.emulateJSON && (u.processData = !1), o.ajax(s.extend(u, n))
        }, i.wrapError = function(e, t, n) {
            return function(r, i) {
                i = r === t ? i : r, e ? e(t, i, n) : t.trigger("error", t, i, n)
            }
        };
        var m = function() {},
            g = function(e, t, n) {
                var r;
                return r = t && t.hasOwnProperty("constructor") ? t.constructor : function() {
                    e.apply(this, arguments)
                }, s.extend(r, e), m.prototype = e.prototype, r.prototype = new m, t && s.extend(r.prototype, t), n && s.extend(r, n), r.prototype.constructor = r, r.__super__ = e.prototype, r
            },
            y = function(e, t) {
                return !e || !e[t] ? null : s.isFunction(e[t]) ? e[t]() : e[t]
            },
            b = function() {
                throw Error('A "url" property or function must be specified')
            }
    }.call(this),
    function(e, t, n) {
        function s(e, r) {
            this.wrapper = typeof e == "string" ? t.querySelector(e) : e, this.scroller = this.wrapper.children[0], this.scrollerStyle = this.scroller.style, this.options = {
                zoomMin: 1,
                zoomMax: 4,
                startZoom: 1,
                resizeScrollbars: !0,
                mouseWheelSpeed: 20,
                snapThreshold: .334,
                startX: 0,
                startY: 0,
                scrollY: !0,
                directionLockThreshold: 5,
                momentum: !0,
                bounce: !0,
                bounceTime: 600,
                bounceEasing: "",
                preventDefault: !0,
                preventDefaultException: {
                    tagName: /^(INPUT|TEXTAREA|BUTTON|SELECT)$/
                },
                HWCompositing: !0,
                useTransition: !0,
                useTransform: !0
            };
            for (var s in r) this.options[s] = r[s];
            this.translateZ = this.options.HWCompositing && i.hasPerspective ? " translateZ(0)" : "", this.options.useTransition = i.hasTransition && this.options.useTransition, this.options.useTransform = i.hasTransform && this.options.useTransform, this.options.eventPassthrough = this.options.eventPassthrough === !0 ? "vertical" : this.options.eventPassthrough, this.options.preventDefault = !this.options.eventPassthrough && this.options.preventDefault, this.options.scrollY = this.options.eventPassthrough == "vertical" ? !1 : this.options.scrollY, this.options.scrollX = this.options.eventPassthrough == "horizontal" ? !1 : this.options.scrollX, this.options.freeScroll = this.options.freeScroll && !this.options.eventPassthrough, this.options.directionLockThreshold = this.options.eventPassthrough ? 0 : this.options.directionLockThreshold, this.options.bounceEasing = typeof this.options.bounceEasing == "string" ? i.ease[this.options.bounceEasing] || i.ease.circular : this.options.bounceEasing, this.options.resizePolling = this.options.resizePolling === undefined ? 60 : this.options.resizePolling, this.options.tap === !0 && (this.options.tap = "tap"), this.options.shrinkScrollbars == "scale" && (this.options.useTransition = !1), this.options.invertWheelDirection = this.options.invertWheelDirection ? -1 : 1, this.x = 0, this.y = 0, this.directionX = 0, this.directionY = 0, this._events = {}, this.scale = n.min(n.max(this.options.startZoom, this.options.zoomMin), this.options.zoomMax), this._init(), this.refresh(), this.scrollTo(this.options.startX, this.options.startY), this.enable()
        }

        function o(e, n, r) {
            var i = t.createElement("div"),
                s = t.createElement("div");
            return r === !0 && (i.style.cssText = "position:absolute;z-index:9999", s.style.cssText = "-webkit-box-sizing:border-box;-moz-box-sizing:border-box;box-sizing:border-box;position:absolute;background:rgba(0,0,0,0.5);border:1px solid rgba(255,255,255,0.9);border-radius:3px"), s.className = "iScrollIndicator", e == "h" ? (r === !0 && (i.style.cssText += ";height:7px;left:2px;right:2px;bottom:0", s.style.height = "100%"), i.className = "iScrollHorizontalScrollbar") : (r === !0 && (i.style.cssText += ";width:7px;bottom:2px;top:2px;right:1px", s.style.width = "100%"), i.className = "iScrollVerticalScrollbar"), i.style.cssText += ";overflow:hidden", n || (i.style.pointerEvents = "none"), i.appendChild(s), i
        }

        function u(n, r) {
            this.wrapper = typeof r.el == "string" ? t.querySelector(r.el) : r.el, this.wrapperStyle = this.wrapper.style, this.indicator = this.wrapper.children[0], this.indicatorStyle = this.indicator.style, this.scroller = n, this.options = {
                listenX: !0,
                listenY: !0,
                interactive: !1,
                resize: !0,
                defaultScrollbars: !1,
                shrink: !1,
                fade: !1,
                speedRatioX: 0,
                speedRatioY: 0
            };
            for (var s in r) this.options[s] = r[s];
            this.sizeRatioX = 1, this.sizeRatioY = 1, this.maxPosX = 0, this.maxPosY = 0, this.options.interactive && (this.options.disableTouch || (i.addEvent(this.indicator, "touchstart", this), i.addEvent(e, "touchend", this)), this.options.disablePointer || (i.addEvent(this.indicator, i.prefixPointerEvent("pointerdown"), this), i.addEvent(e, i.prefixPointerEvent("pointerup"), this)), this.options.disableMouse || (i.addEvent(this.indicator, "mousedown", this), i.addEvent(e, "mouseup", this))), this.options.fade && (this.wrapperStyle[i.style.transform] = this.scroller.translateZ, this.wrapperStyle[i.style.transitionDuration] = i.isBadAndroid ? "0.001s" : "0ms", this.wrapperStyle.opacity = "0")
        }
        var r = e.requestAnimationFrame || e.webkitRequestAnimationFrame || e.mozRequestAnimationFrame || e.oRequestAnimationFrame || e.msRequestAnimationFrame || function(t) {
                e.setTimeout(t, 1e3 / 60)
            },
            i = function() {
                function o(e) {
                    return s === !1 ? !1 : s === "" ? e : s + e.charAt(0).toUpperCase() + e.substr(1)
                }
                var r = {},
                    i = t.createElement("div").style,
                    s = function() {
                        var e = ["t", "webkitT", "MozT", "msT", "OT"],
                            t, n = 0,
                            r = e.length;
                        for (; n < r; n++) {
                            t = e[n] + "ransform";
                            if (t in i) return e[n].substr(0, e[n].length - 1)
                        }
                        return !1
                    }();
                r.getTime = Date.now || function() {
                    return (new Date).getTime()
                }, r.extend = function(e, t) {
                    for (var n in t) e[n] = t[n]
                }, r.addEvent = function(e, t, n, r) {
                    e.addEventListener(t, n, !!r)
                }, r.removeEvent = function(e, t, n, r) {
                    e.removeEventListener(t, n, !!r)
                }, r.prefixPointerEvent = function(t) {
                    return e.MSPointerEvent ? "MSPointer" + t.charAt(9).toUpperCase() + t.substr(10) : t
                }, r.momentum = function(e, t, r, i, s, o) {
                    var u = e - t,
                        a = n.abs(u) / r,
                        f, l;
                    return o = o === undefined ? 6e-4 : o, f = e + a * a / (2 * o) * (u < 0 ? -1 : 1), l = a / o, f < i ? (f = s ? i - s / 2.5 * (a / 8) : i, u = n.abs(f - e), l = u / a) : f > 0 && (f = s ? s / 2.5 * (a / 8) : 0, u = n.abs(e) + f, l = u / a), {
                        destination: n.round(f),
                        duration: l
                    }
                };
                var u = o("transform");
                return r.extend(r, {
                    hasTransform: u !== !1,
                    hasPerspective: o("perspective") in i,
                    hasTouch: "ontouchstart" in e,
                    hasPointer: e.PointerEvent || e.MSPointerEvent,
                    hasTransition: o("transition") in i
                }), r.isBadAndroid = /Android /.test(e.navigator.appVersion) && !/Chrome\/\d/.test(e.navigator.appVersion), r.extend(r.style = {}, {
                    transform: u,
                    transitionTimingFunction: o("transitionTimingFunction"),
                    transitionDuration: o("transitionDuration"),
                    transitionDelay: o("transitionDelay"),
                    transformOrigin: o("transformOrigin")
                }), r.hasClass = function(e, t) {
                    var n = new RegExp("(^|\\s)" + t + "(\\s|$)");
                    return n.test(e.className)
                }, r.addClass = function(e, t) {
                    if (r.hasClass(e, t)) return;
                    var n = e.className.split(" ");
                    n.push(t), e.className = n.join(" ")
                }, r.removeClass = function(e, t) {
                    if (!r.hasClass(e, t)) return;
                    var n = new RegExp("(^|\\s)" + t + "(\\s|$)", "g");
                    e.className = e.className.replace(n, " ")
                }, r.offset = function(e) {
                    var t = -e.offsetLeft,
                        n = -e.offsetTop;
                    while (e = e.offsetParent) t -= e.offsetLeft, n -= e.offsetTop;
                    return {
                        left: t,
                        top: n
                    }
                }, r.preventDefaultException = function(e, t) {
                    for (var n in t)
                        if (t[n].test(e[n])) return !0;
                    return !1
                }, r.extend(r.eventType = {}, {
                    touchstart: 1,
                    touchmove: 1,
                    touchend: 1,
                    mousedown: 2,
                    mousemove: 2,
                    mouseup: 2,
                    pointerdown: 3,
                    pointermove: 3,
                    pointerup: 3,
                    MSPointerDown: 3,
                    MSPointerMove: 3,
                    MSPointerUp: 3
                }), r.extend(r.ease = {}, {
                    quadratic: {
                        style: "cubic-bezier(0.25, 0.46, 0.45, 0.94)",
                        fn: function(e) {
                            return e * (2 - e)
                        }
                    },
                    circular: {
                        style: "cubic-bezier(0.1, 0.57, 0.1, 1)",
                        fn: function(e) {
                            return n.sqrt(1 - --e * e)
                        }
                    },
                    back: {
                        style: "cubic-bezier(0.175, 0.885, 0.32, 1.275)",
                        fn: function(e) {
                            var t = 4;
                            return (e -= 1) * e * ((t + 1) * e + t) + 1
                        }
                    },
                    bounce: {
                        style: "",
                        fn: function(e) {
                            return (e /= 1) < 1 / 2.75 ? 7.5625 * e * e : e < 2 / 2.75 ? 7.5625 * (e -= 1.5 / 2.75) * e + .75 : e < 2.5 / 2.75 ? 7.5625 * (e -= 2.25 / 2.75) * e + .9375 : 7.5625 * (e -= 2.625 / 2.75) * e + .984375
                        }
                    },
                    elastic: {
                        style: "",
                        fn: function(e) {
                            var t = .22,
                                r = .4;
                            return e === 0 ? 0 : e == 1 ? 1 : r * n.pow(2, -10 * e) * n.sin((e - t / 4) * 2 * n.PI / t) + 1
                        }
                    }
                }), r.tap = function(e, n) {
                    var r = t.createEvent("Event");
                    r.initEvent(n, !0, !0), r.pageX = e.pageX, r.pageY = e.pageY, e.target.dispatchEvent(r)
                }, r.click = function(e) {
                    var n = e.target,
                        r;
                    /(SELECT|INPUT|TEXTAREA)/i.test(n.tagName) || (r = t.createEvent("MouseEvents"), r.initMouseEvent("click", !0, !0, e.view, 1, n.screenX, n.screenY, n.clientX, n.clientY, e.ctrlKey, e.altKey, e.shiftKey, e.metaKey, 0, null), r._constructed = !0, n.dispatchEvent(r))
                }, r
            }();
        s.prototype = {
            version: "5.1.3",
            _init: function() {
                this._initEvents(), this.options.zoom && this._initZoom(), (this.options.scrollbars || this.options.indicators) && this._initIndicators(), this.options.mouseWheel && this._initWheel(), this.options.snap && this._initSnap(), this.options.keyBindings && this._initKeys()
            },
            destroy: function() {
                this._initEvents(!0), this._execEvent("destroy")
            },
            _transitionEnd: function(e) {
                if (e.target != this.scroller || !this.isInTransition) return;
                this._transitionTime(), this.resetPosition(this.options.bounceTime) || (this.isInTransition = !1, this._execEvent("scrollEnd"))
            },
            _start: function(e) {
                if (i.eventType[e.type] != 1 && e.button !== 0) return;
                if (!this.enabled || this.initiated && i.eventType[e.type] !== this.initiated) return;
                this.options.preventDefault && !i.isBadAndroid && !i.preventDefaultException(e.target, this.options.preventDefaultException) && e.preventDefault();
                var t = e.touches ? e.touches[0] : e,
                    r;
                this.initiated = i.eventType[e.type], this.moved = !1, this.distX = 0, this.distY = 0, this.directionX = 0, this.directionY = 0, this.directionLocked = 0, this._transitionTime(), this.startTime = i.getTime(), this.options.useTransition && this.isInTransition ? (this.isInTransition = !1, r = this.getComputedPosition(), this._translate(n.round(r.x), n.round(r.y)), this._execEvent("scrollEnd")) : !this.options.useTransition && this.isAnimating && (this.isAnimating = !1, this._execEvent("scrollEnd")), this.startX = this.x, this.startY = this.y, this.absStartX = this.x, this.absStartY = this.y, this.pointX = t.pageX, this.pointY = t.pageY, this._execEvent("beforeScrollStart")
            },
            _move: function(e) {
                if (!this.enabled || i.eventType[e.type] !== this.initiated) return;
                this.options.preventDefault && e.preventDefault();
                var t = e.touches ? e.touches[0] : e,
                    r = t.pageX - this.pointX,
                    s = t.pageY - this.pointY,
                    o = i.getTime(),
                    u, a, f, l;
                this.pointX = t.pageX, this.pointY = t.pageY, this.distX += r, this.distY += s, f = n.abs(this.distX), l = n.abs(this.distY);
                if (o - this.endTime > 300 && f < 10 && l < 10) return;
                !this.directionLocked && !this.options.freeScroll && (f > l + this.options.directionLockThreshold ? this.directionLocked = "h" : l >= f + this.options.directionLockThreshold ? this.directionLocked = "v" : this.directionLocked = "n");
                if (this.directionLocked == "h") {
                    if (this.options.eventPassthrough == "vertical") e.preventDefault();
                    else if (this.options.eventPassthrough == "horizontal") {
                        this.initiated = !1;
                        return
                    }
                    s = 0
                } else if (this.directionLocked == "v") {
                    if (this.options.eventPassthrough == "horizontal") e.preventDefault();
                    else if (this.options.eventPassthrough == "vertical") {
                        this.initiated = !1;
                        return
                    }
                    r = 0
                }
                r = this.hasHorizontalScroll ? r : 0, s = this.hasVerticalScroll ? s : 0, u = this.x + r, a = this.y + s;
                if (u > 0 || u < this.maxScrollX) u = this.options.bounce ? this.x + r / 3 : u > 0 ? 0 : this.maxScrollX;
                if (a > 0 || a < this.maxScrollY) a = this.options.bounce ? this.y + s / 3 : a > 0 ? 0 : this.maxScrollY;
                this.directionX = r > 0 ? -1 : r < 0 ? 1 : 0, this.directionY = s > 0 ? -1 : s < 0 ? 1 : 0, this.moved || this._execEvent("scrollStart"), this.moved = !0, this._translate(u, a), o - this.startTime > 300 && (this.startTime = o, this.startX = this.x, this.startY = this.y)
            },
            _end: function(e) {
                if (!this.enabled || i.eventType[e.type] !== this.initiated) return;
                this.options.preventDefault && !i.preventDefaultException(e.target, this.options.preventDefaultException) && e.preventDefault();
                var t = e.changedTouches ? e.changedTouches[0] : e,
                    r, s, o = i.getTime() - this.startTime,
                    u = n.round(this.x),
                    a = n.round(this.y),
                    f = n.abs(u - this.startX),
                    l = n.abs(a - this.startY),
                    c = 0,
                    h = "";
                this.isInTransition = 0, this.initiated = 0, this.endTime = i.getTime();
                if (this.resetPosition(this.options.bounceTime)) return;
                this.scrollTo(u, a);
                if (!this.moved) {
                    this.options.tap && i.tap(e, this.options.tap), this.options.click && i.click(e), this._execEvent("scrollCancel");
                    return
                }
                if (this._events.flick && o < 200 && f < 100 && l < 100) {
                    this._execEvent("flick");
                    return
                }
                this.options.momentum && o < 300 && (r = this.hasHorizontalScroll ? i.momentum(this.x, this.startX, o, this.maxScrollX, this.options.bounce ? this.wrapperWidth : 0, this.options.deceleration) : {
                    destination: u,
                    duration: 0
                }, s = this.hasVerticalScroll ? i.momentum(this.y, this.startY, o, this.maxScrollY, this.options.bounce ?
                    this.wrapperHeight : 0, this.options.deceleration) : {
                    destination: a,
                    duration: 0
                }, u = r.destination, a = s.destination, c = n.max(r.duration, s.duration), this.isInTransition = 1);
                if (this.options.snap) {
                    var p = this._nearestSnap(u, a);
                    this.currentPage = p, c = this.options.snapSpeed || n.max(n.max(n.min(n.abs(u - p.x), 1e3), n.min(n.abs(a - p.y), 1e3)), 300), u = p.x, a = p.y, this.directionX = 0, this.directionY = 0, h = this.options.bounceEasing
                }
                if (u != this.x || a != this.y) {
                    if (u > 0 || u < this.maxScrollX || a > 0 || a < this.maxScrollY) h = i.ease.quadratic;
                    this.scrollTo(u, a, c, h);
                    return
                }
                this._execEvent("scrollEnd")
            },
            _resize: function() {
                var e = this;
                clearTimeout(this.resizeTimeout), this.resizeTimeout = setTimeout(function() {
                    e.refresh()
                }, this.options.resizePolling)
            },
            resetPosition: function(e) {
                var t = this.x,
                    n = this.y;
                return e = e || 0, !this.hasHorizontalScroll || this.x > 0 ? t = 0 : this.x < this.maxScrollX && (t = this.maxScrollX), !this.hasVerticalScroll || this.y > 0 ? n = 0 : this.y < this.maxScrollY && (n = this.maxScrollY), t == this.x && n == this.y ? !1 : (this.scrollTo(t, n, e, this.options.bounceEasing), !0)
            },
            disable: function() {
                this.enabled = !1
            },
            enable: function() {
                this.enabled = !0
            },
            refresh: function() {
                var e = this.wrapper.offsetHeight;
                this.wrapperWidth = this.wrapper.clientWidth, this.wrapperHeight = this.wrapper.clientHeight, this.scrollerWidth = n.round(this.scroller.offsetWidth * this.scale), this.scrollerHeight = n.round(this.scroller.offsetHeight * this.scale), this.maxScrollX = this.wrapperWidth - this.scrollerWidth, this.maxScrollY = this.wrapperHeight - this.scrollerHeight, this.hasHorizontalScroll = this.options.scrollX && this.maxScrollX < 0, this.hasVerticalScroll = this.options.scrollY && this.maxScrollY < 0, this.hasHorizontalScroll || (this.maxScrollX = 0, this.scrollerWidth = this.wrapperWidth), this.hasVerticalScroll || (this.maxScrollY = 0, this.scrollerHeight = this.wrapperHeight), this.endTime = 0, this.directionX = 0, this.directionY = 0, this.wrapperOffset = i.offset(this.wrapper), this._execEvent("refresh"), this.resetPosition()
            },
            on: function(e, t) {
                this._events[e] || (this._events[e] = []), this._events[e].push(t)
            },
            off: function(e, t) {
                if (!this._events[e]) return;
                var n = this._events[e].indexOf(t);
                n > -1 && this._events[e].splice(n, 1)
            },
            _execEvent: function(e) {
                if (!this._events[e]) return;
                var t = 0,
                    n = this._events[e].length;
                if (!n) return;
                for (; t < n; t++) this._events[e][t].apply(this, [].slice.call(arguments, 1))
            },
            scrollBy: function(e, t, n, r) {
                e = this.x + e, t = this.y + t, n = n || 0, this.scrollTo(e, t, n, r)
            },
            scrollTo: function(e, t, n, r) {
                r = r || i.ease.circular, this.isInTransition = this.options.useTransition && n > 0, !n || this.options.useTransition && r.style ? (this._transitionTimingFunction(r.style), this._transitionTime(n), this._translate(e, t)) : this._animate(e, t, n, r.fn)
            },
            scrollToElement: function(e, t, r, s, o) {
                e = e.nodeType ? e : this.scroller.querySelector(e);
                if (!e) return;
                var u = i.offset(e);
                u.left -= this.wrapperOffset.left, u.top -= this.wrapperOffset.top, r === !0 && (r = n.round(e.offsetWidth / 2 - this.wrapper.offsetWidth / 2)), s === !0 && (s = n.round(e.offsetHeight / 2 - this.wrapper.offsetHeight / 2)), u.left -= r || 0, u.top -= s || 0, u.left = u.left > 0 ? 0 : u.left < this.maxScrollX ? this.maxScrollX : u.left, u.top = u.top > 0 ? 0 : u.top < this.maxScrollY ? this.maxScrollY : u.top, t = t === undefined || t === null || t === "auto" ? n.max(n.abs(this.x - u.left), n.abs(this.y - u.top)) : t, this.scrollTo(u.left, u.top, t, o)
            },
            _transitionTime: function(e) {
                e = e || 0, this.scrollerStyle[i.style.transitionDuration] = e + "ms", !e && i.isBadAndroid && (this.scrollerStyle[i.style.transitionDuration] = "0.001s");
                if (this.indicators)
                    for (var t = this.indicators.length; t--;) this.indicators[t].transitionTime(e)
            },
            _transitionTimingFunction: function(e) {
                this.scrollerStyle[i.style.transitionTimingFunction] = e;
                if (this.indicators)
                    for (var t = this.indicators.length; t--;) this.indicators[t].transitionTimingFunction(e)
            },
            _translate: function(e, t) {
                this.options.useTransform ? this.scrollerStyle[i.style.transform] = "translate(" + e + "px," + t + "px) scale(" + this.scale + ") " + this.translateZ : (e = n.round(e), t = n.round(t), this.scrollerStyle.left = e + "px", this.scrollerStyle.top = t + "px"), this.x = e, this.y = t;
                if (this.indicators)
                    for (var r = this.indicators.length; r--;) this.indicators[r].updatePosition()
            },
            _initEvents: function(t) {
                var n = t ? i.removeEvent : i.addEvent,
                    r = this.options.bindToWrapper ? this.wrapper : e;
                n(e, "orientationchange", this), n(e, "resize", this), this.options.click && n(this.wrapper, "click", this, !0), this.options.disableMouse || (n(this.wrapper, "mousedown", this), n(r, "mousemove", this), n(r, "mousecancel", this), n(r, "mouseup", this)), i.hasPointer && !this.options.disablePointer && (n(this.wrapper, i.prefixPointerEvent("pointerdown"), this), n(r, i.prefixPointerEvent("pointermove"), this), n(r, i.prefixPointerEvent("pointercancel"), this), n(r, i.prefixPointerEvent("pointerup"), this)), i.hasTouch && !this.options.disableTouch && (n(this.wrapper, "touchstart", this), n(r, "touchmove", this), n(r, "touchcancel", this), n(r, "touchend", this)), n(this.scroller, "transitionend", this), n(this.scroller, "webkitTransitionEnd", this), n(this.scroller, "oTransitionEnd", this), n(this.scroller, "MSTransitionEnd", this)
            },
            getComputedPosition: function() {
                var t = e.getComputedStyle(this.scroller, null),
                    n, r;
                return this.options.useTransform ? (t = t[i.style.transform].split(")")[0].split(", "), n = +(t[12] || t[4]), r = +(t[13] || t[5])) : (n = +t.left.replace(/[^-\d.]/g, ""), r = +t.top.replace(/[^-\d.]/g, "")), {
                    x: n,
                    y: r
                }
            },
            _initIndicators: function() {
                function a(e) {
                    for (var t = i.indicators.length; t--;) e.call(i.indicators[t])
                }
                var e = this.options.interactiveScrollbars,
                    t = typeof this.options.scrollbars != "string",
                    n = [],
                    r, i = this;
                this.indicators = [], this.options.scrollbars && (this.options.scrollY && (r = {
                    el: o("v", e, this.options.scrollbars),
                    interactive: e,
                    defaultScrollbars: !0,
                    customStyle: t,
                    resize: this.options.resizeScrollbars,
                    shrink: this.options.shrinkScrollbars,
                    fade: this.options.fadeScrollbars,
                    listenX: !1
                }, this.wrapper.appendChild(r.el), n.push(r)), this.options.scrollX && (r = {
                    el: o("h", e, this.options.scrollbars),
                    interactive: e,
                    defaultScrollbars: !0,
                    customStyle: t,
                    resize: this.options.resizeScrollbars,
                    shrink: this.options.shrinkScrollbars,
                    fade: this.options.fadeScrollbars,
                    listenY: !1
                }, this.wrapper.appendChild(r.el), n.push(r))), this.options.indicators && (n = n.concat(this.options.indicators));
                for (var s = n.length; s--;) this.indicators.push(new u(this, n[s]));
                this.options.fadeScrollbars && (this.on("scrollEnd", function() {
                    a(function() {
                        this.fade()
                    })
                }), this.on("scrollCancel", function() {
                    a(function() {
                        this.fade()
                    })
                }), this.on("scrollStart", function() {
                    a(function() {
                        this.fade(1)
                    })
                }), this.on("beforeScrollStart", function() {
                    a(function() {
                        this.fade(1, !0)
                    })
                })), this.on("refresh", function() {
                    a(function() {
                        this.refresh()
                    })
                }), this.on("destroy", function() {
                    a(function() {
                        this.destroy()
                    }), delete this.indicators
                })
            },
            _initZoom: function() {
                this.scrollerStyle[i.style.transformOrigin] = "0 0"
            },
            _zoomStart: function(e) {
                var t = n.abs(e.touches[0].pageX - e.touches[1].pageX),
                    r = n.abs(e.touches[0].pageY - e.touches[1].pageY);
                this.touchesDistanceStart = n.sqrt(t * t + r * r), this.startScale = this.scale, this.originX = n.abs(e.touches[0].pageX + e.touches[1].pageX) / 2 + this.wrapperOffset.left - this.x, this.originY = n.abs(e.touches[0].pageY + e.touches[1].pageY) / 2 + this.wrapperOffset.top - this.y, this._execEvent("zoomStart")
            },
            _zoom: function(e) {
                if (!this.enabled || i.eventType[e.type] !== this.initiated) return;
                this.options.preventDefault && e.preventDefault();
                var t = n.abs(e.touches[0].pageX - e.touches[1].pageX),
                    r = n.abs(e.touches[0].pageY - e.touches[1].pageY),
                    s = n.sqrt(t * t + r * r),
                    o = 1 / this.touchesDistanceStart * s * this.startScale,
                    u, a, f;
                this.scaled = !0, o < this.options.zoomMin ? o = .5 * this.options.zoomMin * n.pow(2, o / this.options.zoomMin) : o > this.options.zoomMax && (o = 2 * this.options.zoomMax * n.pow(.5, this.options.zoomMax / o)), u = o / this.startScale, a = this.originX - this.originX * u + this.startX, f = this.originY - this.originY * u + this.startY, this.scale = o, this.scrollTo(a, f, 0)
            },
            _zoomEnd: function(e) {
                if (!this.enabled || i.eventType[e.type] !== this.initiated) return;
                this.options.preventDefault && e.preventDefault();
                var t, n, r;
                this.isInTransition = 0, this.initiated = 0, this.scale > this.options.zoomMax ? this.scale = this.options.zoomMax : this.scale < this.options.zoomMin && (this.scale = this.options.zoomMin), this.refresh(), r = this.scale / this.startScale, t = this.originX - this.originX * r + this.startX, n = this.originY - this.originY * r + this.startY, t > 0 ? t = 0 : t < this.maxScrollX && (t = this.maxScrollX), n > 0 ? n = 0 : n < this.maxScrollY && (n = this.maxScrollY), (this.x != t || this.y != n) && this.scrollTo(t, n, this.options.bounceTime), this.scaled = !1, this._execEvent("zoomEnd")
            },
            zoom: function(e, t, n, r) {
                e < this.options.zoomMin ? e = this.options.zoomMin : e > this.options.zoomMax && (e = this.options.zoomMax);
                if (e == this.scale) return;
                var i = e / this.scale;
                t = t === undefined ? this.wrapperWidth / 2 : t, n = n === undefined ? this.wrapperHeight / 2 : n, r = r === undefined ? 300 : r, t = t + this.wrapperOffset.left - this.x, n = n + this.wrapperOffset.top - this.y, t = t - t * i + this.x, n = n - n * i + this.y, this.scale = e, this.refresh(), t > 0 ? t = 0 : t < this.maxScrollX && (t = this.maxScrollX), n > 0 ? n = 0 : n < this.maxScrollY && (n = this.maxScrollY), this.scrollTo(t, n, r)
            },
            _wheelZoom: function(e) {
                var t, r, i = this;
                clearTimeout(this.wheelTimeout), this.wheelTimeout = setTimeout(function() {
                    i._execEvent("zoomEnd")
                }, 400);
                if ("deltaX" in e) t = -e.deltaY / n.abs(e.deltaY);
                else if ("wheelDeltaX" in e) t = e.wheelDeltaY / n.abs(e.wheelDeltaY);
                else if ("wheelDelta" in e) t = e.wheelDelta / n.abs(e.wheelDelta);
                else {
                    if (!("detail" in e)) return;
                    t = -e.detail / n.abs(e.wheelDelta)
                }
                r = this.scale + t / 5, this.zoom(r, e.pageX, e.pageY, 0)
            },
            _initWheel: function() {
                i.addEvent(this.wrapper, "wheel", this), i.addEvent(this.wrapper, "mousewheel", this), i.addEvent(this.wrapper, "DOMMouseScroll", this), this.on("destroy", function() {
                    i.removeEvent(this.wrapper, "wheel", this), i.removeEvent(this.wrapper, "mousewheel", this), i.removeEvent(this.wrapper, "DOMMouseScroll", this)
                })
            },
            _wheel: function(e) {
                if (!this.enabled) return;
                e.preventDefault(), e.stopPropagation();
                var t, r, i, s, o = this;
                this.wheelTimeout === undefined && o._execEvent("scrollStart"), clearTimeout(this.wheelTimeout), this.wheelTimeout = setTimeout(function() {
                    o._execEvent("scrollEnd"), o.wheelTimeout = undefined
                }, 400);
                if ("deltaX" in e) e.deltaMode === 1 ? (t = -e.deltaX * this.options.mouseWheelSpeed, r = -e.deltaY * this.options.mouseWheelSpeed) : (t = -e.deltaX, r = -e.deltaY);
                else if ("wheelDeltaX" in e) t = e.wheelDeltaX / 120 * this.options.mouseWheelSpeed, r = e.wheelDeltaY / 120 * this.options.mouseWheelSpeed;
                else if ("wheelDelta" in e) t = r = e.wheelDelta / 120 * this.options.mouseWheelSpeed;
                else {
                    if (!("detail" in e)) return;
                    t = r = -e.detail / 3 * this.options.mouseWheelSpeed
                }
                t *= this.options.invertWheelDirection, r *= this.options.invertWheelDirection, this.hasVerticalScroll || (t = r, r = 0);
                if (this.options.snap) {
                    i = this.currentPage.pageX, s = this.currentPage.pageY, t > 0 ? i-- : t < 0 && i++, r > 0 ? s-- : r < 0 && s++, this.goToPage(i, s);
                    return
                }
                i = this.x + n.round(this.hasHorizontalScroll ? t : 0), s = this.y + n.round(this.hasVerticalScroll ? r : 0), i > 0 ? i = 0 : i < this.maxScrollX && (i = this.maxScrollX), s > 0 ? s = 0 : s < this.maxScrollY && (s = this.maxScrollY), this.scrollTo(i, s, 0)
            },
            _initSnap: function() {
                this.currentPage = {}, typeof this.options.snap == "string" && (this.options.snap = this.scroller.querySelectorAll(this.options.snap)), this.on("refresh", function() {
                    var e = 0,
                        t, r = 0,
                        i, s, o, u = 0,
                        a, f = this.options.snapStepX || this.wrapperWidth,
                        l = this.options.snapStepY || this.wrapperHeight,
                        c;
                    this.pages = [];
                    if (!this.wrapperWidth || !this.wrapperHeight || !this.scrollerWidth || !this.scrollerHeight) return;
                    if (this.options.snap === !0) {
                        s = n.round(f / 2), o = n.round(l / 2);
                        while (u > -this.scrollerWidth) {
                            this.pages[e] = [], t = 0, a = 0;
                            while (a > -this.scrollerHeight) this.pages[e][t] = {
                                x: n.max(u, this.maxScrollX),
                                y: n.max(a, this.maxScrollY),
                                width: f,
                                height: l,
                                cx: u - s,
                                cy: a - o
                            }, a -= l, t++;
                            u -= f, e++
                        }
                    } else {
                        c = this.options.snap, t = c.length, i = -1;
                        for (; e < t; e++) {
                            if (e === 0 || c[e].offsetLeft <= c[e - 1].offsetLeft) r = 0, i++;
                            this.pages[r] || (this.pages[r] = []), u = n.max(-c[e].offsetLeft, this.maxScrollX), a = n.max(-c[e].offsetTop, this.maxScrollY), s = u - n.round(c[e].offsetWidth / 2), o = a - n.round(c[e].offsetHeight / 2), this.pages[r][i] = {
                                x: u,
                                y: a,
                                width: c[e].offsetWidth,
                                height: c[e].offsetHeight,
                                cx: s,
                                cy: o
                            }, u > this.maxScrollX && r++
                        }
                    }
                    this.goToPage(this.currentPage.pageX || 0, this.currentPage.pageY || 0, 0), this.options.snapThreshold % 1 === 0 ? (this.snapThresholdX = this.options.snapThreshold, this.snapThresholdY = this.options.snapThreshold) : (this.snapThresholdX = n.round(this.pages[this.currentPage.pageX][this.currentPage.pageY].width * this.options.snapThreshold), this.snapThresholdY = n.round(this.pages[this.currentPage.pageX][this.currentPage.pageY].height * this.options.snapThreshold))
                }), this.on("flick", function() {
                    var e = this.options.snapSpeed || n.max(n.max(n.min(n.abs(this.x - this.startX), 1e3), n.min(n.abs(this.y - this.startY), 1e3)), 300);
                    this.goToPage(this.currentPage.pageX + this.directionX, this.currentPage.pageY + this.directionY, e)
                })
            },
            _nearestSnap: function(e, t) {
                if (!this.pages.length) return {
                    x: 0,
                    y: 0,
                    pageX: 0,
                    pageY: 0
                };
                var r = 0,
                    i = this.pages.length,
                    s = 0;
                if (n.abs(e - this.absStartX) < this.snapThresholdX && n.abs(t - this.absStartY) < this.snapThresholdY) return this.currentPage;
                e > 0 ? e = 0 : e < this.maxScrollX && (e = this.maxScrollX), t > 0 ? t = 0 : t < this.maxScrollY && (t = this.maxScrollY);
                for (; r < i; r++)
                    if (e >= this.pages[r][0].cx) {
                        e = this.pages[r][0].x;
                        break
                    } i = this.pages[r].length;
                for (; s < i; s++)
                    if (t >= this.pages[0][s].cy) {
                        t = this.pages[0][s].y;
                        break
                    } return r == this.currentPage.pageX && (r += this.directionX, r < 0 ? r = 0 : r >= this.pages.length && (r = this.pages.length - 1), e = this.pages[r][0].x), s == this.currentPage.pageY && (s += this.directionY, s < 0 ? s = 0 : s >= this.pages[0].length && (s = this.pages[0].length - 1), t = this.pages[0][s].y), {
                    x: e,
                    y: t,
                    pageX: r,
                    pageY: s
                }
            },
            goToPage: function(e, t, r, i) {
                i = i || this.options.bounceEasing, e >= this.pages.length ? e = this.pages.length - 1 : e < 0 && (e = 0), t >= this.pages[e].length ? t = this.pages[e].length - 1 : t < 0 && (t = 0);
                var s = this.pages[e][t].x,
                    o = this.pages[e][t].y;
                r = r === undefined ? this.options.snapSpeed || n.max(n.max(n.min(n.abs(s - this.x), 1e3), n.min(n.abs(o - this.y), 1e3)), 300) : r, this.currentPage = {
                    x: s,
                    y: o,
                    pageX: e,
                    pageY: t
                }, this.scrollTo(s, o, r, i)
            },
            next: function(e, t) {
                var n = this.currentPage.pageX,
                    r = this.currentPage.pageY;
                n++, n >= this.pages.length && this.hasVerticalScroll && (n = 0, r++), this.goToPage(n, r, e, t)
            },
            prev: function(e, t) {
                var n = this.currentPage.pageX,
                    r = this.currentPage.pageY;
                n--, n < 0 && this.hasVerticalScroll && (n = 0, r--), this.goToPage(n, r, e, t)
            },
            _initKeys: function(t) {
                var n = {
                        pageUp: 33,
                        pageDown: 34,
                        end: 35,
                        home: 36,
                        left: 37,
                        up: 38,
                        right: 39,
                        down: 40
                    },
                    r;
                if (typeof this.options.keyBindings == "object")
                    for (r in this.options.keyBindings) typeof this.options.keyBindings[r] == "string" && (this.options.keyBindings[r] = this.options.keyBindings[r].toUpperCase().charCodeAt(0));
                else this.options.keyBindings = {};
                for (r in n) this.options.keyBindings[r] = this.options.keyBindings[r] || n[r];
                i.addEvent(e, "keydown", this), this.on("destroy", function() {
                    i.removeEvent(e, "keydown", this)
                })
            },
            _key: function(e) {
                if (!this.enabled) return;
                var t = this.options.snap,
                    r = t ? this.currentPage.pageX : this.x,
                    s = t ? this.currentPage.pageY : this.y,
                    o = i.getTime(),
                    u = this.keyTime || 0,
                    a = .25,
                    f;
                this.options.useTransition && this.isInTransition && (f = this.getComputedPosition(), this._translate(n.round(f.x), n.round(f.y)), this.isInTransition = !1), this.keyAcceleration = o - u < 200 ? n.min(this.keyAcceleration + a, 50) : 0;
                switch (e.keyCode) {
                    case this.options.keyBindings.pageUp:
                        this.hasHorizontalScroll && !this.hasVerticalScroll ? r += t ? 1 : this.wrapperWidth : s += t ? 1 : this.wrapperHeight;
                        break;
                    case this.options.keyBindings.pageDown:
                        this.hasHorizontalScroll && !this.hasVerticalScroll ? r -= t ? 1 : this.wrapperWidth : s -= t ? 1 : this.wrapperHeight;
                        break;
                    case this.options.keyBindings.end:
                        r = t ? this.pages.length - 1 : this.maxScrollX, s = t ? this.pages[0].length - 1 : this.maxScrollY;
                        break;
                    case this.options.keyBindings.home:
                        r = 0, s = 0;
                        break;
                    case this.options.keyBindings.left:
                        r += t ? -1 : 5 + this.keyAcceleration >> 0;
                        break;
                    case this.options.keyBindings.up:
                        s += t ? 1 : 5 + this.keyAcceleration >> 0;
                        break;
                    case this.options.keyBindings.right:
                        r -= t ? -1 : 5 + this.keyAcceleration >> 0;
                        break;
                    case this.options.keyBindings.down:
                        s -= t ? 1 : 5 + this.keyAcceleration >> 0;
                        break;
                    default:
                        return
                }
                if (t) {
                    this.goToPage(r, s);
                    return
                }
                r > 0 ? (r = 0, this.keyAcceleration = 0) : r < this.maxScrollX && (r = this.maxScrollX, this.keyAcceleration = 0), s > 0 ? (s = 0, this.keyAcceleration = 0) : s < this.maxScrollY && (s = this.maxScrollY, this.keyAcceleration = 0), this.scrollTo(r, s, 0), this.keyTime = o
            },
            _animate: function(e, t, n, s) {
                function c() {
                    var h = i.getTime(),
                        p, d, v;
                    if (h >= l) {
                        o.isAnimating = !1, o._translate(e, t), o.resetPosition(o.options.bounceTime) || o._execEvent("scrollEnd");
                        return
                    }
                    h = (h - f) / n, v = s(h), p = (e - u) * v + u, d = (t - a) * v + a, o._translate(p, d), o.isAnimating && r(c)
                }
                var o = this,
                    u = this.x,
                    a = this.y,
                    f = i.getTime(),
                    l = f + n;
                this.isAnimating = !0, c()
            },
            handleEvent: function(e) {
                switch (e.type) {
                    case "touchstart":
                    case "pointerdown":
                    case "MSPointerDown":
                    case "mousedown":
                        this._start(e), this.options.zoom && e.touches && e.touches.length > 1 && this._zoomStart(e);
                        break;
                    case "touchmove":
                    case "pointermove":
                    case "MSPointerMove":
                    case "mousemove":
                        if (this.options.zoom && e.touches && e.touches[1]) {
                            this._zoom(e);
                            return
                        }
                        this._move(e);
                        break;
                    case "touchend":
                    case "pointerup":
                    case "MSPointerUp":
                    case "mouseup":
                    case "touchcancel":
                    case "pointercancel":
                    case "MSPointerCancel":
                    case "mousecancel":
                        if (this.scaled) {
                            this._zoomEnd(e);
                            return
                        }
                        this._end(e);
                        break;
                    case "orientationchange":
                    case "resize":
                        this._resize();
                        break;
                    case "transitionend":
                    case "webkitTransitionEnd":
                    case "oTransitionEnd":
                    case "MSTransitionEnd":
                        this._transitionEnd(e);
                        break;
                    case "wheel":
                    case "DOMMouseScroll":
                    case "mousewheel":
                        if (this.options.wheelAction == "zoom") {
                            this._wheelZoom(e);
                            return
                        }
                        this._wheel(e);
                        break;
                    case "keydown":
                        this._key(e)
                }
            }
        }, u.prototype = {
            handleEvent: function(e) {
                switch (e.type) {
                    case "touchstart":
                    case "pointerdown":
                    case "MSPointerDown":
                    case "mousedown":
                        this._start(e);
                        break;
                    case "touchmove":
                    case "pointermove":
                    case "MSPointerMove":
                    case "mousemove":
                        this._move(e);
                        break;
                    case "touchend":
                    case "pointerup":
                    case "MSPointerUp":
                    case "mouseup":
                    case "touchcancel":
                    case "pointercancel":
                    case "MSPointerCancel":
                    case "mousecancel":
                        this._end(e)
                }
            },
            destroy: function() {
                this.options.interactive && (i.removeEvent(this.indicator, "touchstart", this), i.removeEvent(this.indicator, i.prefixPointerEvent("pointerdown"), this), i.removeEvent(this.indicator, "mousedown", this), i.removeEvent(e, "touchmove", this), i.removeEvent(e, i.prefixPointerEvent("pointermove"), this), i.removeEvent(e, "mousemove", this), i.removeEvent(e, "touchend", this), i.removeEvent(e, i.prefixPointerEvent("pointerup"), this), i.removeEvent(e, "mouseup", this)), this.options.defaultScrollbars && this.wrapper.parentNode.removeChild(this.wrapper)
            },
            _start: function(t) {
                var n = t.touches ? t.touches[0] : t;
                t.preventDefault(), t.stopPropagation(), this.transitionTime(), this.initiated = !0, this.moved = !1, this.lastPointX = n.pageX, this.lastPointY = n.pageY, this.startTime = i.getTime(), this.options.disableTouch || i.addEvent(e, "touchmove", this), this.options.disablePointer || i.addEvent(e, i.prefixPointerEvent("pointermove"), this), this.options.disableMouse || i.addEvent(e, "mousemove", this), this.scroller._execEvent("beforeScrollStart")
            },
            _move: function(e) {
                var t = e.touches ? e.touches[0] : e,
                    n, r, s, o, u = i.getTime();
                this.moved || this.scroller._execEvent("scrollStart"), this.moved = !0, n = t.pageX - this.lastPointX, this.lastPointX = t.pageX, r = t.pageY - this.lastPointY, this.lastPointY = t.pageY, s = this.x + n, o = this.y + r, this._pos(s, o), e.preventDefault(), e.stopPropagation()
            },
            _end: function(t) {
                if (!this.initiated) return;
                this.initiated = !1, t.preventDefault(), t.stopPropagation(), i.removeEvent(e, "touchmove", this), i.removeEvent(e, i.prefixPointerEvent("pointermove"), this), i.removeEvent(e, "mousemove", this);
                if (this.scroller.options.snap) {
                    var r = this.scroller._nearestSnap(this.scroller.x, this.scroller.y),
                        s = this.options.snapSpeed || n.max(n.max(n.min(n.abs(this.scroller.x - r.x), 1e3), n.min(n.abs(this.scroller.y - r.y), 1e3)), 300);
                    if (this.scroller.x != r.x || this.scroller.y != r.y) this.scroller.directionX = 0, this.scroller.directionY = 0, this.scroller.currentPage = r, this.scroller.scrollTo(r.x, r.y, s, this.scroller.options.bounceEasing)
                }
                this.moved && this.scroller._execEvent("scrollEnd")
            },
            transitionTime: function(e) {
                e = e || 0, this.indicatorStyle[i.style.transitionDuration] = e + "ms", !e && i.isBadAndroid && (this.indicatorStyle[i.style.transitionDuration] = "0.001s")
            },
            transitionTimingFunction: function(e) {
                this.indicatorStyle[i.style.transitionTimingFunction] = e
            },
            refresh: function() {
                this.transitionTime(), this.options.listenX && !this.options.listenY ? this.indicatorStyle.display = this.scroller.hasHorizontalScroll ? "block" : "none" : this.options.listenY && !this.options.listenX ? this.indicatorStyle.display = this.scroller.hasVerticalScroll ? "block" : "none" : this.indicatorStyle.display = this.scroller.hasHorizontalScroll || this.scroller.hasVerticalScroll ? "block" : "none", this.scroller.hasHorizontalScroll && this.scroller.hasVerticalScroll ? (i.addClass(this.wrapper, "iScrollBothScrollbars"), i.removeClass(this.wrapper, "iScrollLoneScrollbar"), this.options.defaultScrollbars && this.options.customStyle && (this.options.listenX ? this.wrapper.style.right = "8px" : this.wrapper.style.bottom = "8px")) : (i.removeClass(this.wrapper, "iScrollBothScrollbars"), i.addClass(this.wrapper, "iScrollLoneScrollbar"), this.options.defaultScrollbars && this.options.customStyle && (this.options.listenX ? this.wrapper.style.right = "2px" : this.wrapper.style.bottom = "2px"));
                var e = this.wrapper.offsetHeight;
                this.options.listenX && (this.wrapperWidth = this.wrapper.clientWidth, this.options.resize ? (this.indicatorWidth = n.max(n.round(this.wrapperWidth * this.wrapperWidth / (this.scroller.scrollerWidth || this.wrapperWidth || 1)), 8), this.indicatorStyle.width = this.indicatorWidth + "px") : this.indicatorWidth = this.indicator.clientWidth, this.maxPosX = this.wrapperWidth - this.indicatorWidth, this.options.shrink == "clip" ? (this.minBoundaryX = -this.indicatorWidth + 8, this.maxBoundaryX = this.wrapperWidth - 8) : (this.minBoundaryX = 0, this.maxBoundaryX = this.maxPosX), this.sizeRatioX = this.options.speedRatioX || this.scroller.maxScrollX && this.maxPosX / this.scroller.maxScrollX), this.options.listenY && (this.wrapperHeight = this.wrapper.clientHeight, this.options.resize ? (this.indicatorHeight = n.max(n.round(this.wrapperHeight * this.wrapperHeight / (this.scroller.scrollerHeight || this.wrapperHeight || 1)), 8), this.indicatorStyle.height = this.indicatorHeight + "px") : this.indicatorHeight = this.indicator.clientHeight, this.maxPosY = this.wrapperHeight - this.indicatorHeight, this.options.shrink == "clip" ? (this.minBoundaryY = -this.indicatorHeight + 8, this.maxBoundaryY = this.wrapperHeight - 8) : (this.minBoundaryY = 0, this.maxBoundaryY = this.maxPosY), this.maxPosY = this.wrapperHeight - this.indicatorHeight, this.sizeRatioY = this.options.speedRatioY || this.scroller.maxScrollY && this.maxPosY / this.scroller.maxScrollY), this.updatePosition()
            },
            updatePosition: function() {
                var e = this.options.listenX && n.round(this.sizeRatioX * this.scroller.x) || 0,
                    t = this.options.listenY && n.round(this.sizeRatioY * this.scroller.y) || 0;
                this.options.ignoreBoundaries || (e < this.minBoundaryX ? (this.options.shrink == "scale" && (this.width = n.max(this.indicatorWidth + e, 8), this.indicatorStyle.width = this.width + "px"), e = this.minBoundaryX) : e > this.maxBoundaryX ? this.options.shrink == "scale" ? (this.width = n.max(this.indicatorWidth - (e - this.maxPosX), 8), this.indicatorStyle.width = this.width + "px", e = this.maxPosX + this.indicatorWidth - this.width) : e = this.maxBoundaryX : this.options.shrink == "scale" && this.width != this.indicatorWidth && (this.width = this.indicatorWidth, this.indicatorStyle.width = this.width + "px"), t < this.minBoundaryY ? (this.options.shrink == "scale" && (this.height = n.max(this.indicatorHeight + t * 3, 8), this.indicatorStyle.height = this.height + "px"), t = this.minBoundaryY) : t > this.maxBoundaryY ? this.options.shrink == "scale" ? (this.height = n.max(this.indicatorHeight - (t - this.maxPosY) * 3, 8), this.indicatorStyle.height = this.height + "px", t = this.maxPosY + this.indicatorHeight - this.height) : t = this.maxBoundaryY : this.options.shrink == "scale" && this.height != this.indicatorHeight && (this.height = this.indicatorHeight, this.indicatorStyle.height = this.height + "px")), this.x = e, this.y = t, this.scroller.options.useTransform ? this.indicatorStyle[i.style.transform] = "translate(" + e + "px," + t + "px)" + this.scroller.translateZ : (this.indicatorStyle.left = e + "px", this.indicatorStyle.top = t + "px")
            },
            _pos: function(e, t) {
                e < 0 ? e = 0 : e > this.maxPosX && (e = this.maxPosX), t < 0 ? t = 0 : t > this.maxPosY && (t = this.maxPosY), e = this.options.listenX ? n.round(e / this.sizeRatioX) : this.scroller.x, t = this.options.listenY ? n.round(t / this.sizeRatioY) : this.scroller.y, this.scroller.scrollTo(e, t)
            },
            fade: function(e, t) {
                if (t && !this.visible) return;
                clearTimeout(this.fadeTimeout), this.fadeTimeout = null;
                var n = e ? 250 : 500,
                    r = e ? 0 : 300;
                e = e ? "1" : "0", this.wrapperStyle[i.style.transitionDuration] = n + "ms", this.fadeTimeout = setTimeout(function(e) {
                    this.wrapperStyle.opacity = e, this.visible = +e
                }.bind(this, e), r)
            }
        }, s.utils = i, typeof module != "undefined" && module.exports ? module.exports = s : e.IScroll = s
    }(window, document, Math), (window.jQuery || window.Zepto) && function(e) {
        e.fn.Swipe = function(t) {
            return this.each(function() {
                e(this).data("Swipe", new Swipe(e(this)[0], t))
            })
        }
    }(window.jQuery || window.Zepto),
    function(e, t) {
        typeof exports == "object" && typeof module != "undefined" ? module.exports = t() : typeof define == "function" && define.amd ? define(t) : e.PullToRefresh = t()
    }(this, function() {
        function e() {
            return '\n<div class="__PREFIX__box">\n  <div class="__PREFIX__content">\n    <div class="__PREFIX__icon"></div>\n    <div class="__PREFIX__text"></div>\n  </div>\n</div>'
        }

        function t() {
            return ".__PREFIX__ptr svg { width: 24px; height: 24px; } .__PREFIX__ptr--refresh svg { background: red; width: 24px; height: 24px; } .__PREFIX__ptr {\n pointer-events: none;\n  font-size: 0.85em;\n  font-weight: bold;\n  top: 0;\n  height: 0;\n  transition: height 0.3s, min-height 0.3s, opacity 200ms;\n  text-align: center;\n  width: 100%;\n    align-items: flex-end;\n  align-content: stretch;\n}\n.__PREFIX__box {\n  padding: 10px;\n  flex-basis: 100%;\n}\n.__PREFIX__pull {\n  transition: none;\n}\n.__PREFIX__text {\n  margin-top: 0.33em;\n  color: rgba(0, 0, 0, 0.3);\n}\n.__PREFIX__icon {\n  color: rgba(0, 0, 0, 0.3);\n  transition: transform 0.3s;\n}\n.__PREFIX__top {\n  touch-action: pan-x pan-down pinch-zoom;\n}\n.__PREFIX__release {    \n}\n"
        }

        function u() {
            function t(t) {
                var n = i.handlers.filter(function(e) {
                    return e.contains(t.target)
                })[0];
                i.enable = !!n, n && i.state === "pending" && (e = o.setupDOM(n), n.shouldPullToRefresh() && (i.pullStartY = t.touches[0].screenY), clearTimeout(i.timeout), o.update(n))
            }

            function n(t) {
                if (!(e && e.ptrElement && i.enable)) return;
                i.pullStartY ? i.pullMoveY = t.touches[0].screenY : e.shouldPullToRefresh() && (i.pullStartY = t.touches[0].screenY);
                if (i.state === "refreshing") {
                    e.shouldPullToRefresh() && i.pullStartY < i.pullMoveY && t.preventDefault();
                    return
                }
                i.state === "pending" && (e.ptrElement.classList.add(e.classPrefix + "pull"), i.state = "pulling", o.update(e)), i.pullStartY && i.pullMoveY && (i.dist = i.pullMoveY - i.pullStartY), i.distExtra = i.dist - e.distIgnore, i.distExtra > 0 && (t.preventDefault(), e.ptrElement.style[e.cssProp] = i.distResisted + "px", i.distResisted = e.resistanceFunction(i.distExtra / e.distThreshold) * Math.min(e.distMax, i.distExtra), i.state === "pulling" && i.distResisted > e.distThreshold && (e.ptrElement.classList.add(e.classPrefix + "release"), i.state = "releasing", o.update(e)), i.state === "releasing" && i.distResisted < e.distThreshold && (e.ptrElement.classList.remove(e.classPrefix + "release"), i.state = "pulling", o.update(e)))
            }

            function r() {
                if (!(e && e.ptrElement && i.enable)) return;
                if (i.state === "releasing" && i.distResisted > e.distThreshold) i.state = "refreshing", e.ptrElement.style[e.cssProp] = e.distReload + "px", e.ptrElement.classList.add(e.classPrefix + "refresh"), i.timeout = setTimeout(function() {
                    var t = e.onRefresh(function() {
                        return o.onReset(e)
                    });
                    t && typeof t.then == "function" && t.then(function() {
                        return o.onReset(e)
                    }), !t && !e.onRefresh.length && o.onReset(e)
                }, e.refreshTimeout);
                else {
                    if (i.state === "refreshing") return;
                    e.ptrElement.style[e.cssProp] = "0px", i.state = "pending"
                }
                o.update(e), e.ptrElement.classList.remove(e.classPrefix + "release"), e.ptrElement.classList.remove(e.classPrefix + "pull"), i.pullStartY = i.pullMoveY = null, i.dist = i.distResisted = 0
            }

            function s() {
                e && e.mainElement.classList.toggle(e.classPrefix + "top", e.shouldPullToRefresh())
            }
            var e, u = i.supportsPassive ? {
                passive: i.passive || !1
            } : undefined;
            return window.addEventListener("touchend", r), window.addEventListener("touchstart", t), window.addEventListener("touchmove", n, u), window.addEventListener("scroll", s), {
                onTouchEnd: r,
                onTouchStart: t,
                onTouchMove: n,
                onScroll: s,
                destroy: function() {
                    window.removeEventListener("touchstart", t), window.removeEventListener("touchend", r), window.removeEventListener("touchmove", n, u), window.removeEventListener("scroll", s)
                }
            }
        }

        function a(e) {
            var t = {};
            return Object.keys(n).forEach(function(r) {
                t[r] = e[r] || n[r]
            }), t.refreshTimeout = typeof e.refreshTimeout == "number" ? e.refreshTimeout : n.refreshTimeout, r.forEach(function(e) {
                typeof t[e] == "string" && (t[e] = document.querySelector(t[e]))
            }), i.events || (i.events = u()), t.contains = function(e) {
                return t.triggerElement.contains(e)
            }, t.destroy = function() {
                clearTimeout(i.timeout), i.handlers.splice(t.offset, 1)
            }, t
        }
        var n = {
                distThreshold: 40,
                distMax: 80,
                distReload: 50,
                distIgnore: 0,
                bodyOffset: 20,
                mainElement: "body",
                triggerElement: "body",
                ptrElement: ".ptr",
                classPrefix: "ptr--",
                cssProp: "min-height",
                iconArrow: '<?xml version="1.0" encoding="UTF-8"?><svg version="1.1" viewBox="0 0 35 35" xmlns="http://www.w3.org/2000/svg"><g fill="none" fill-rule="evenodd"><g transform="translate(-8 -8)" fill="#000"><path d="m25.5 17c-0.825 0-1.5-0.675-1.5-1.5v-6c0-0.825 0.675-1.5 1.5-1.5s1.5 0.675 1.5 1.5v6c0 0.825-0.675 1.5-1.5 1.5" opacity=".65"/><path d="m29.75 18.139c-0.7145-0.4125-0.9615-1.3345-0.549-2.049l3-5.196c0.4125-0.7145 1.3345-0.9615 2.049-0.549s0.9615 1.3345 0.549 2.049l-3 5.196c-0.4125 0.7145-1.3345 0.9615-2.049 0.549" opacity=".75"/><path d="m32.861 21.25c-0.4125-0.7145-0.1655-1.6365 0.549-2.049l5.196-3c0.7145-0.4125 1.6365-0.1655 2.049 0.549s0.1655 1.6365-0.549 2.049l-5.196 3c-0.7145 0.4125-1.6365 0.1655-2.049-0.549" opacity=".85"/><path d="m34 25.5c0-0.825 0.675-1.5 1.5-1.5h6c0.825 0 1.5 0.675 1.5 1.5s-0.675 1.5-1.5 1.5h-6c-0.825 0-1.5-0.675-1.5-1.5" opacity=".9"/><path d="m32.861 29.75c0.4125-0.7145 1.3345-0.9615 2.049-0.549l5.196 3c0.7145 0.4125 0.9615 1.3345 0.549 2.049s-1.3345 0.9615-2.049 0.549l-5.196-3c-0.7145-0.4125-0.9615-1.3345-0.549-2.049" opacity=".2"/><path d="m29.75 32.861c0.7145-0.4125 1.6365-0.1655 2.049 0.549l3 5.196c0.4125 0.7145 0.1655 1.6365-0.549 2.049s-1.6365 0.1655-2.049-0.549l-3-5.196c-0.4125-0.7145-0.1655-1.6365 0.549-2.049" opacity=".25"/><path d="m25.5 34c0.825 0 1.5 0.675 1.5 1.5v6c0 0.825-0.675 1.5-1.5 1.5s-1.5-0.675-1.5-1.5v-6c0-0.825 0.675-1.5 1.5-1.5" opacity=".3"/><path d="m21.25 32.861c0.7145 0.4125 0.9615 1.3345 0.549 2.049l-3 5.196c-0.4125 0.7145-1.3345 0.9615-2.049 0.549s-0.9615-1.3345-0.549-2.049l3-5.196c0.4125-0.7145 1.3345-0.9615 2.049-0.549" opacity=".35"/><path d="m18.139 29.75c0.4125 0.7145 0.1655 1.6365-0.549 2.049l-5.196 3c-0.7145 0.4125-1.6365 0.1655-2.049-0.549s-0.1655-1.6365 0.549-2.049l5.196-3c0.7145-0.4125 1.6365-0.1655 2.049 0.549" opacity=".4"/><path d="m17 25.5c0 0.825-0.675 1.5-1.5 1.5h-6c-0.825 0-1.5-0.675-1.5-1.5s0.675-1.5 1.5-1.5h6c0.825 0 1.5 0.675 1.5 1.5" opacity=".45"/><path d="m18.139 21.25c-0.4125 0.7145-1.3345 0.9615-2.049 0.549l-5.196-3c-0.7145-0.4125-0.9615-1.3345-0.549-2.049s1.3345-0.9615 2.049-0.549l5.196 3c0.7145 0.4125 0.9615 1.3345 0.549 2.049" opacity=".5"/><path d="m21.25 18.139c-0.7145 0.4125-1.6365 0.1655-2.049-0.549l-3-5.196c-0.4125-0.7145-0.1655-1.6365 0.549-2.049s1.6365-0.1655 2.049 0.549l3 5.196c0.4125 0.7145 0.1655 1.6365-0.549 2.049" opacity=".55"/></g></g></svg>',
                iconRefreshing: '<?xml version="1.0" encoding="UTF-8"?><svg version="1.1" viewBox="0 0 35 35" xmlns="http://www.w3.org/2000/svg"><g fill="none" fill-rule="evenodd"><g transform="translate(-8 -8)" fill="#000"><path d="m25.5 17c-0.825 0-1.5-0.675-1.5-1.5v-6c0-0.825 0.675-1.5 1.5-1.5s1.5 0.675 1.5 1.5v6c0 0.825-0.675 1.5-1.5 1.5" opacity=".65"/><path d="m29.75 18.139c-0.7145-0.4125-0.9615-1.3345-0.549-2.049l3-5.196c0.4125-0.7145 1.3345-0.9615 2.049-0.549s0.9615 1.3345 0.549 2.049l-3 5.196c-0.4125 0.7145-1.3345 0.9615-2.049 0.549" opacity=".75"/><path d="m32.861 21.25c-0.4125-0.7145-0.1655-1.6365 0.549-2.049l5.196-3c0.7145-0.4125 1.6365-0.1655 2.049 0.549s0.1655 1.6365-0.549 2.049l-5.196 3c-0.7145 0.4125-1.6365 0.1655-2.049-0.549" opacity=".85"/><path d="m34 25.5c0-0.825 0.675-1.5 1.5-1.5h6c0.825 0 1.5 0.675 1.5 1.5s-0.675 1.5-1.5 1.5h-6c-0.825 0-1.5-0.675-1.5-1.5" opacity=".9"/><path d="m32.861 29.75c0.4125-0.7145 1.3345-0.9615 2.049-0.549l5.196 3c0.7145 0.4125 0.9615 1.3345 0.549 2.049s-1.3345 0.9615-2.049 0.549l-5.196-3c-0.7145-0.4125-0.9615-1.3345-0.549-2.049" opacity=".2"/><path d="m29.75 32.861c0.7145-0.4125 1.6365-0.1655 2.049 0.549l3 5.196c0.4125 0.7145 0.1655 1.6365-0.549 2.049s-1.6365 0.1655-2.049-0.549l-3-5.196c-0.4125-0.7145-0.1655-1.6365 0.549-2.049" opacity=".25"/><path d="m25.5 34c0.825 0 1.5 0.675 1.5 1.5v6c0 0.825-0.675 1.5-1.5 1.5s-1.5-0.675-1.5-1.5v-6c0-0.825 0.675-1.5 1.5-1.5" opacity=".3"/><path d="m21.25 32.861c0.7145 0.4125 0.9615 1.3345 0.549 2.049l-3 5.196c-0.4125 0.7145-1.3345 0.9615-2.049 0.549s-0.9615-1.3345-0.549-2.049l3-5.196c0.4125-0.7145 1.3345-0.9615 2.049-0.549" opacity=".35"/><path d="m18.139 29.75c0.4125 0.7145 0.1655 1.6365-0.549 2.049l-5.196 3c-0.7145 0.4125-1.6365 0.1655-2.049-0.549s-0.1655-1.6365 0.549-2.049l5.196-3c0.7145-0.4125 1.6365-0.1655 2.049 0.549" opacity=".4"/><path d="m17 25.5c0 0.825-0.675 1.5-1.5 1.5h-6c-0.825 0-1.5-0.675-1.5-1.5s0.675-1.5 1.5-1.5h6c0.825 0 1.5 0.675 1.5 1.5" opacity=".45"/><path d="m18.139 21.25c-0.4125 0.7145-1.3345 0.9615-2.049 0.549l-5.196-3c-0.7145-0.4125-0.9615-1.3345-0.549-2.049s1.3345-0.9615 2.049-0.549l5.196 3c0.7145 0.4125 0.9615 1.3345 0.549 2.049" opacity=".5"/><path d="m21.25 18.139c-0.7145 0.4125-1.6365 0.1655-2.049-0.549l-3-5.196c-0.4125-0.7145-0.1655-1.6365 0.549-2.049s1.6365-0.1655 2.049 0.549l3 5.196c0.4125 0.7145 0.1655 1.6365-0.549 2.049" opacity=".55"/></g></g></svg>',
                instructionsPullToRefresh: "",
                instructionsReleaseToRefresh: "",
                instructionsRefreshing: "",
                refreshTimeout: 500,
                getMarkup: e,
                getStyles: t,
                onInit: function() {},
                onRefresh: function() {
                    return location.reload()
                },
                resistanceFunction: function(e) {
                    return Math.min(1, e / 2.5)
                },
                shouldPullToRefresh: function() {
                    return !window.scrollY
                }
            },
            r = ["mainElement", "ptrElement", "triggerElement"],
            i = {
                pullStartY: null,
                pullMoveY: null,
                handlers: [],
                styleEl: null,
                events: null,
                dist: 0,
                state: "pending",
                timeout: null,
                distResisted: 0,
                supportsPassive: !1
            };
        try {
            window.addEventListener("test", null, {
                get passive() {
                    i.supportsPassive = !0
                }
            })
        } catch (s) {}
        var o = {
                setupDOM: function(t) {
                    if (!t.ptrElement) {
                        var n = document.createElement("div");
                        t.mainElement !== document.body ? t.mainElement.parentNode.insertBefore(n, t.mainElement) : document.body.insertBefore(n, document.body.firstChild), n.classList.add(t.classPrefix + "ptr"), n.innerHTML = t.getMarkup().replace(/__PREFIX__/g, t.classPrefix), t.ptrElement = n, typeof t.onInit == "function" && t.onInit(t), i.styleEl || (i.styleEl = document.createElement("style"), i.styleEl.setAttribute("id", "pull-to-refresh-js-style"), document.head.appendChild(i.styleEl)), i.styleEl.textContent = t.getStyles().replace(/__PREFIX__/g, t.classPrefix).replace(/\s+/g, " ")
                    }
                    return t
                },
                onReset: function(t) {
                    t.ptrElement.classList.remove(t.classPrefix + "refresh"), t.ptrElement.style[t.cssProp] = "0px", setTimeout(function() {
                        t.ptrElement && t.ptrElement.parentNode && (t.ptrElement.parentNode.removeChild(t.ptrElement), t.ptrElement = null), i.state = "pending"
                    }, t.refreshTimeout)
                },
                update: function(t) {
                    var n = t.ptrElement.querySelector("." + t.classPrefix + "icon"),
                        r = t.ptrElement.querySelector("." + t.classPrefix + "text");
                    n && (i.state === "refreshing" ? n.innerHTML = t.iconRefreshing : n.innerHTML = t.iconArrow);
                    if (r) {
                        i.state === "releasing" && (r.innerHTML = t.instructionsReleaseToRefresh);
                        if (i.state === "pulling" || i.state === "pending") r.innerHTML = t.instructionsPullToRefresh;
                        i.state === "refreshing" && (r.innerHTML = t.instructionsRefreshing)
                    }
                }
            },
            f = {
                setPassiveMode: function(t) {
                    i.passive = t
                },
                destroyAll: function() {
                    i.events && (i.events.destroy(), i.events = null), i.handlers.forEach(function(e) {
                        e.destroy()
                    })
                },
                init: function(t) {
                    t === void 0 && (t = {});
                    var n = a(t);
                    return n.offset = i.handlers.push(n) - 1, n
                },
                _: {
                    setupHandler: a,
                    setupEvents: u,
                    setupDOM: o.setupDOM,
                    onReset: o.onReset,
                    update: o.update
                }
            };
        return f
    }),
    function(e) {
        function t(t) {
            return "".trim ? t.val().trim() : e.trim(t.val())
        }
        e.fn.isHappy = function(n) {
            function s(t) {
                var n;
                return u(t.message) ? n = t.message.call() : n = t.message, e('<span id="' + t.id + '" class="inline-js-message">' + n + "</span>")
            }

            function o() {
                var e = !1,
                    t, i;
                for (t = 0, i = r.length; t < i; t += 1) r[t].testValid(!0) || (e = !0);
                if (e) return u(n.unHappy) && n.unHappy(), !1;
                if (n.testMode) return window.console && console.warn("would have submitted"), !1
            }

            function u(e) {
                return !!(e && e.constructor && e.call && e.apply)
            }

            function a(i, o) {
                var a = e(o, n.selectorScope);
                r.push(a), a.testValid = function(n) {
                    var r = {
                            message: i.message,
                            id: o.slice(1) + "_unhappy"
                        },
                        f = s(r),
                        l = "#" + r.id,
                        c, h = e(this),
                        p, r = !1,
                        d, v = !!h.get(0).attributes.getNamedItem("required") || i.required,
                        m = a.attr("type") === "password",
                        g = u(i.arg) ? i.arg() : i.arg;
                    u(i.clean) ? c = i.clean(h.val()) : !i.trim && !m ? c = t(h) : c = h.val(), h.val(c), p = u(i.test), h.attr("type") != "checkbox" ? v === !0 && c.length === 0 ? r = !0 : p && (r = !i.test(c, g)) : v == 1 && h.prop("checked") ? r = !1 : r = !0;
                    var y = e(l),
                        b = !1;
                    return y.length > 0 && (b = !0), r ? (i.errorPosition ? (h.addClass("unhappy"), i.errorPosition.placement == "append" && (b ? y.html(i.message) : e(i.errorPosition.selector).append(f))) : b ? y.html(i.message) : h.addClass("unhappy").after(f), h.closest(".field").addClass("unhappy"), !1) : (d = f.get(0), d.parentNode && d.parentNode.removeChild(d), h.removeClass("unhappy"), h.closest(".field").removeClass("unhappy"), h.closest("tr").find(".inline-js-message").remove(), !0)
                }, a.attr("type") == "checkbox" ? a.bind("change", a.testValid) : a.bind(n.when || "blur", a.testValid)
            }
            var r = [],
                i;
            for (i in n.fields) a(n.fields[i], i);
            return n.submitButton ? e(n.submitButton).click(o) : this.bind("submit", o), this
        }
    }(this.jQuery || this.Zepto),
    function(e) {
        typeof define == "function" && define.amd ? define(["jquery"], e) : typeof module == "object" && module.exports ? module.exports = function(t, n) {
            return e(n), n
        } : e(Zepto)
    }(function(e) {
        "use strict";
        var t = e(document),
            n = e(window),
            r = "selectric",
            i = "Input Items Open Disabled TempShow HideSelect Wrapper Focus Hover Responsive Above Scroll Group GroupLabel",
            s = ".sl",
            o = ["a", "e", "i", "o", "u", "n", "c", "y"],
            u = [/[\xE0-\xE5]/g, /[\xE8-\xEB]/g, /[\xEC-\xEF]/g, /[\xF2-\xF6]/g, /[\xF9-\xFC]/g, /[\xF1]/g, /[\xE7]/g, /[\xFD-\xFF]/g],
            a = function(t, n) {
                var r = this;
                r.element = t, r.$element = e(t), r.state = {
                    multiple: !!r.$element.attr("multiple"),
                    enabled: !1,
                    opened: !1,
                    currValue: -1,
                    selectedIdx: -1,
                    highlightedIdx: -1
                }, r.eventTriggers = {
                    open: r.open,
                    close: r.close,
                    destroy: r.destroy,
                    refresh: r.refresh,
                    init: r.init
                }, r.init(n)
            };
        a.prototype = {
            utils: {
                isMobile: function() {
                    return /android|ip(hone|od|ad)/i.test(navigator.userAgent)
                },
                escapeRegExp: function(e) {
                    return e.replace(/[.*+?^${}()|[\]\\]/g, "\\$&")
                },
                replaceDiacritics: function(e) {
                    var t = u.length;
                    while (t--) e = e.toLowerCase().replace(u[t], o[t]);
                    return e
                },
                format: function(e) {
                    var t = arguments;
                    return ("" + e).replace(/\{(?:(\d+)|(\w+))\}/g, function(e, n, r) {
                        return r && t[1] ? t[1][r] : t[n]
                    })
                },
                nextEnabledItem: function(e, t) {
                    while (e[t = (t + 1) % e.length].disabled);
                    return t
                },
                previousEnabledItem: function(e, t) {
                    while (e[t = (t > 0 ? t : e.length) - 1].disabled);
                    return t
                },
                toDash: function(e) {
                    return e.replace(/([a-z0-9])([A-Z])/g, "$1-$2").toLowerCase()
                },
                triggerCallback: function(t, n) {
                    var i = n.element,
                        s = n.options["on" + t],
                        o = [i].concat([].slice.call(arguments).slice(1));
                    e.isFunction(s) && s.apply(i, o), e(i).trigger(r + "-" + this.toDash(t), o)
                },
                arrayToClassname: function(t) {
                    var n = e.grep(t, function(e) {
                        return !!e
                    });
                    return e.trim(n.join(" "))
                }
            },
            init: function(t) {
                var n = this;
                n.options = e.extend(!0, {}, e.fn[r].defaults, n.options, t), n.utils.triggerCallback("BeforeInit", n), n.destroy(!0);
                if (n.options.disableOnMobile && n.utils.isMobile()) {
                    n.disableOnMobile = !0;
                    return
                }
                n.classes = n.getClassNames();
                var i = e("<input/>", {
                        "class": n.classes.input,
                        readonly: n.utils.isMobile()
                    }),
                    s = e("<div/>", {
                        "class": n.classes.items,
                        tabindex: -1
                    }),
                    o = e("<div/>", {
                        "class": n.classes.scroll
                    }),
                    u = e("<div/>", {
                        "class": n.classes.prefix,
                        html: n.options.arrowButtonMarkup
                    }),
                    a = e("<span/>", {
                        "class": "label"
                    }),
                    f = n.$element.wrap("<div/>").parent().append(u.prepend(a), s, i),
                    l = e("<div/>", {
                        "class": n.classes.hideselect
                    });
                n.elements = {
                    input: i,
                    items: s,
                    itemsScroll: o,
                    wrapper: u,
                    label: a,
                    outerWrapper: f
                }, n.options.nativeOnMobile && n.utils.isMobile() && (n.elements.input = undefined, l.addClass(n.classes.prefix + "-is-native"), n.$element.on("change", function() {
                    n.refresh()
                })), n.$element.on(n.eventTriggers).wrap(l), n.originalTabindex = n.$element.prop("tabindex"), n.$element.prop("tabindex", -1), n.populate(), n.activate(), n.utils.triggerCallback("Init", n)
            },
            activate: function() {
                var e = this,
                    t = e.elements.items.closest(":visible").children(":hidden").addClass(e.classes.tempshow),
                    n = e.$element.width();
                t.removeClass(e.classes.tempshow), e.utils.triggerCallback("BeforeActivate", e), e.elements.outerWrapper.prop("class", e.utils.arrayToClassname([e.classes.wrapper, e.$element.prop("class").replace(/\S+/g, e.classes.prefix + "-$&"), e.options.responsive ? e.classes.responsive : ""])), e.options.inheritOriginalWidth && n > 0 && e.elements.outerWrapper.width(n), e.unbindEvents(), e.$element.prop("disabled") ? (e.elements.outerWrapper.addClass(e.classes.disabled), e.elements.input && e.elements.input.prop("disabled", !0)) : (e.state.enabled = !0, e.elements.outerWrapper.removeClass(e.classes.disabled), e.$li = e.elements.items.removeAttr("style").find("li"), e.bindEvents()), e.utils.triggerCallback("Activate", e)
            },
            getClassNames: function() {
                var t = this,
                    n = t.options.customClass,
                    r = {};
                return e.each(i.split(" "), function(e, i) {
                    var s = n.prefix + i;
                    r[i.toLowerCase()] = n.camelCase ? s : t.utils.toDash(s)
                }), r.prefix = n.prefix, r
            },
            setLabel: function() {
                var t = this,
                    n = t.options.labelBuilder;
                if (t.state.multiple) {
                    var r = e.isArray(t.state.currValue) ? t.state.currValue : [t.state.currValue];
                    r = r.length === 0 ? [0] : r;
                    var i = e.map(r, function(n) {
                        return e.grep(t.lookupItems, function(e) {
                            return e.index === n
                        })[0]
                    });
                    i = e.grep(i, function(t) {
                        return i.length > 1 || i.length === 0 ? e.trim(t.value) !== "" : t
                    }), i = e.map(i, function(r) {
                        return e.isFunction(n) ? n(r) : t.utils.format(n, r)
                    }), t.options.multiple.maxLabelEntries && (i.length >= t.options.multiple.maxLabelEntries + 1 ? (i = i.slice(0, t.options.multiple.maxLabelEntries), i.push(e.isFunction(n) ? n({
                        text: "..."
                    }) : t.utils.format(n, {
                        text: "..."
                    }))) : i.slice(i.length - 1)), t.elements.label.html(i.join(t.options.multiple.separator))
                } else {
                    var s = t.lookupItems[t.state.currValue];
                    t.elements.label.html(e.isFunction(n) ? n(s) : t.utils.format(n, s))
                }
            },
            populate: function() {
                var t = this,
                    n = t.$element.children(),
                    r = t.$element.find("option"),
                    i = r.filter(":selected"),
                    s = r.index(i),
                    o = 0,
                    u = t.state.multiple ? [] : 0;
                i.length > 1 && t.state.multiple && (s = [], i.each(function() {
                    s.push(e(this).index())
                })), t.state.currValue = ~s ? s : u, t.state.selectedIdx = t.state.currValue, t.state.highlightedIdx = t.state.currValue, t.items = [], t.lookupItems = [], n.length && (n.each(function(n) {
                    var r = e(this);
                    if (r.is("optgroup")) {
                        var i = {
                            element: r,
                            label: r.prop("label"),
                            groupDisabled: r.prop("disabled"),
                            items: []
                        };
                        r.children().each(function(n) {
                            var r = e(this);
                            i.items[n] = t.getItemData(o, r, i.groupDisabled || r.prop("disabled")), t.lookupItems[o] = i.items[n], o++
                        }), t.items[n] = i
                    } else t.items[n] = t.getItemData(o, r, r.prop("disabled")), t.lookupItems[o] = t.items[n], o++
                }), t.setLabel(), t.elements.items.append(t.elements.itemsScroll.html(t.getItemsMarkup(t.items))))
            },
            getItemData: function(t, n, r) {
                var i = this;
                return {
                    index: t,
                    element: n,
                    value: n.val(),
                    className: n.prop("class"),
                    text: n.html(),
                    slug: e.trim(i.utils.replaceDiacritics(n.html())),
                    alt: n.attr("data-alt"),
                    selected: n.prop("selected"),
                    disabled: r
                }
            },
            getItemsMarkup: function(t) {
                var n = this,
                    r = "<ul>";
                return e.isFunction(n.options.listBuilder) && n.options.listBuilder && (t = n.options.listBuilder(t)), e.each(t, function(t, i) {
                    i.label !== undefined ? (r += n.utils.format('<ul class="{1}"><li class="{2}">{3}</li>', n.utils.arrayToClassname([n.classes.group, i.groupDisabled ? "disabled" : "", i.element.prop("class")]), n.classes.grouplabel, i.element.prop("label")), e.each(i.items, function(e, t) {
                        r += n.getItemMarkup(t.index, t)
                    }), r += "</ul>") : r += n.getItemMarkup(i.index, i)
                }), r + "</ul>"
            },
            getItemMarkup: function(t, n) {
                var r = this,
                    i = r.options.optionsItemBuilder,
                    s = {
                        value: n.value,
                        text: n.text,
                        slug: n.slug,
                        index: n.index
                    };
                return r.utils.format('<li data-index="{1}" class="{2}">{3}</li>', t, r.utils.arrayToClassname([n.className, t === r.items.length - 1 ? "last" : "", n.disabled ? "disabled" : "", n.selected ? "selected" : ""]), e.isFunction(i) ? r.utils.format(i(n, this.$element, t), n) : r.utils.format(i, s))
            },
            unbindEvents: function() {
                var e = this;
                e.elements.wrapper.add(e.$element).add(e.elements.outerWrapper).add(e.elements.input).off(s)
            },
            bindEvents: function() {
                var t = this;
                t.elements.outerWrapper.on("mouseenter" + s + " mouseleave" + s, function(n) {
                    e(this).toggleClass(t.classes.hover, n.type === "mouseenter"), t.options.openOnHover && (clearTimeout(t.closeTimer), n.type === "mouseleave" ? t.closeTimer = setTimeout(e.proxy(t.close, t), t.options.hoverIntentTimeout) : t.open())
                }), t.elements.wrapper.on("click" + s, function(e) {
                    t.state.opened ? t.close() : t.open(e)
                });
                if (!t.options.nativeOnMobile || !t.utils.isMobile()) t.$element.on("focus" + s, function() {
                    t.elements.input.focus()
                }), t.elements.input.prop({
                    tabindex: t.originalTabindex,
                    disabled: !1
                }).on("keydown" + s, e.proxy(t.handleKeys, t)).on("focusin" + s, function(e) {
                    t.elements.outerWrapper.addClass(t.classes.focus), t.elements.input.one("blur", function() {
                        t.elements.input.blur()
                    }), t.options.openOnFocus && !t.state.opened && t.open(e)
                }).on("focusout" + s, function() {
                    t.elements.outerWrapper.removeClass(t.classes.focus)
                }).on("input propertychange", function() {
                    var n = t.elements.input.val(),
                        r = new RegExp("^" + t.utils.escapeRegExp(n), "i");
                    clearTimeout(t.resetStr), t.resetStr = setTimeout(function() {
                        t.elements.input.val("")
                    }, t.options.keySearchTimeout), n.length && e.each(t.items, function(e, n) {
                        if (n.disabled) return;
                        if (r.test(n.text) || r.test(n.slug)) {
                            t.highlight(e);
                            return
                        }
                        if (!n.alt) return;
                        var i = n.alt.split("|");
                        for (var s = 0; s < i.length; s++) {
                            if (!i[s]) break;
                            if (r.test(i[s].trim())) {
                                t.highlight(e);
                                return
                            }
                        }
                    })
                });
                t.$li.on({
                    mousedown: function(e) {
                        e.preventDefault(), e.stopPropagation()
                    },
                    click: function() {
                        return t.select(e(this).data("index")), !1
                    }
                })
            },
            handleKeys: function(t) {
                var n = this,
                    r = t.which,
                    i = n.options.keys,
                    s = e.inArray(r, i.previous) > -1,
                    o = e.inArray(r, i.next) > -1,
                    u = e.inArray(r, i.select) > -1,
                    a = e.inArray(r, i.open) > -1,
                    f = n.state.highlightedIdx,
                    l = s && f === 0 || o && f + 1 === n.items.length,
                    c = 0;
                (r === 13 || r === 32) && t.preventDefault();
                if (s || o) {
                    if (!n.options.allowWrap && l) return;
                    s && (c = n.utils.previousEnabledItem(n.lookupItems, f)), o && (c = n.utils.nextEnabledItem(n.lookupItems, f)), n.highlight(c)
                }
                if (u && n.state.opened) {
                    n.select(f), (!n.state.multiple || !n.options.multiple.keepMenuOpen) && n.close();
                    return
                }
                a && !n.state.opened && n.open()
            },
            refresh: function() {
                var e = this;
                e.populate(), e.activate(), e.utils.triggerCallback("Refresh", e)
            },
            setOptionsDimensions: function() {
                var e = this,
                    t = e.elements.items.closest(":visible").children(":hidden").addClass(e.classes.tempshow),
                    n = e.options.maxHeight,
                    r = e.elements.items.outerWidth(),
                    i = e.elements.wrapper.outerWidth() - (r - e.elements.items.width());
                !e.options.expandToItemText || i > r ? e.finalWidth = i : (e.elements.items.css("overflow", "scroll"), e.elements.outerWrapper.width(9e4), e.finalWidth = e.elements.items.width(), e.elements.items.css("overflow", ""), e.elements.outerWrapper.width("")), e.elements.items.width(e.finalWidth).height() > n && e.elements.items.height(n), t.removeClass(e.classes.tempshow)
            },
            isInViewport: function() {
                var e = this;
                if (e.options.forceRenderAbove === !0) e.elements.outerWrapper.addClass(e.classes.above);
                else {
                    var t = n.scrollTop(),
                        r = n.height(),
                        i = e.elements.outerWrapper.offset().top,
                        s = e.elements.outerWrapper.outerHeight(),
                        o = i + s + e.itemsHeight <= t + r,
                        u = i - e.itemsHeight > t,
                        a = !o && u;
                    e.elements.outerWrapper.toggleClass(e.classes.above, a)
                }
            },
            detectItemVisibility: function(t) {
                var n = this,
                    r = n.$li.filter("[data-index]");
                n.state.multiple && (t = e.isArray(t) && t.length === 0 ? 0 : t, t = e.isArray(t) ? Math.min.apply(Math, t) : t);
                var i = r.eq(t).outerHeight(),
                    s = r[t].offsetTop,
                    o = n.elements.itemsScroll.scrollTop(),
                    u = s + i * 2;
                n.elements.itemsScroll.scrollTop(u > o + n.itemsHeight ? u - n.itemsHeight : s - i < o ? s - i : o)
            },
            open: function(n) {
                var i = this;
                if (i.options.nativeOnMobile && i.utils.isMobile()) return !1;
                i.utils.triggerCallback("BeforeOpen", i), n && (n.preventDefault(), i.options.stopPropagation && n.stopPropagation()), i.state.enabled && (i.setOptionsDimensions(), e("." + i.classes.hideselect, "." + i.classes.open).children()[r]("close"), i.state.opened = !0, i.itemsHeight = i.elements.items.outerHeight(), i.itemsInnerHeight = i.elements.items.height(), i.elements.outerWrapper.addClass(i.classes.open), i.elements.input.val(""), n && n.type !== "focusin" && i.elements.input.focus(), setTimeout(function() {
                    t.on("click" + s, e.proxy(i.close, i)).on("scroll" + s, e.proxy(i.isInViewport, i))
                }, 1), i.isInViewport(), i.options.preventWindowScroll && t.on("mousewheel" + s + " DOMMouseScroll" + s, "." + i.classes.scroll, function(t) {
                    var n = t.originalEvent,
                        r = e(this).scrollTop(),
                        s = 0;
                    "detail" in n && (s = n.detail * -1), "wheelDelta" in n && (s = n.wheelDelta), "wheelDeltaY" in n && (s = n.wheelDeltaY), "deltaY" in n && (s = n.deltaY * -1), (r === this.scrollHeight - i.itemsInnerHeight && s < 0 || r === 0 && s > 0) && t.preventDefault()
                }), i.detectItemVisibility(i.state.selectedIdx), i.highlight(i.state.multiple ? -1 : i.state.selectedIdx), i.utils.triggerCallback("Open", i))
            },
            close: function() {
                var e = this;
                e.utils.triggerCallback("BeforeClose", e), t.off(s), e.elements.outerWrapper.removeClass(e.classes.open), e.state.opened = !1, e.utils.triggerCallback("Close", e)
            },
            change: function() {
                var t = this;
                t.utils.triggerCallback("BeforeChange", t), t.state.multiple ? (e.each(t.lookupItems, function(e) {
                    t.lookupItems[e].selected = !1, t.$element.find("option").prop("selected", !1)
                }), e.each(t.state.selectedIdx, function(e, n) {
                    t.lookupItems[n].selected = !0, t.$element.find("option").eq(n).prop("selected", !0)
                }), t.state.currValue = t.state.selectedIdx, t.setLabel(), t.utils.triggerCallback("Change", t)) : t.state.currValue !== t.state.selectedIdx && (t.$element.prop("selectedIndex", t.state.currValue = t.state.selectedIdx).data("value", t.lookupItems[t.state.selectedIdx].text), t.setLabel(), t.utils.triggerCallback("Change", t))
            },
            highlight: function(e) {
                var t = this,
                    n = t.$li.filter("[data-index]").removeClass("highlighted");
                t.utils.triggerCallback("BeforeHighlight", t);
                if (e === undefined || e === -1 || t.lookupItems[e].disabled) return;
                n.eq(t.state.highlightedIdx = e).addClass("highlighted"), t.detectItemVisibility(e), t.utils.triggerCallback("Highlight", t)
            },
            select: function(t) {
                var n = this,
                    r = n.$li.filter("[data-index]");
                n.utils.triggerCallback("BeforeSelect", n, t);
                if (t === undefined || t === -1 || n.lookupItems[t].disabled) return;
                if (n.state.multiple) {
                    n.state.selectedIdx = e.isArray(n.state.selectedIdx) ? n.state.selectedIdx : [n.state.selectedIdx];
                    var i = e.inArray(t, n.state.selectedIdx);
                    i !== -1 ? n.state.selectedIdx.splice(i, 1) : n.state.selectedIdx.push(t), r.removeClass("selected").filter(function(t) {
                        return e.inArray(t, n.state.selectedIdx) !== -1
                    }).addClass("selected")
                } else r.removeClass("selected").eq(n.state.selectedIdx = t).addClass("selected");
                (!n.state.multiple || !n.options.multiple.keepMenuOpen) && n.close(), n.change(), n.utils.triggerCallback("Select", n, t)
            },
            destroy: function(e) {
                var t = this;
                t.state && t.state.enabled && (t.elements.items.add(t.elements.wrapper).add(t.elements.input).remove(), e || t.$element.removeData(r).removeData("value"), t.$element.prop("tabindex", t.originalTabindex).off(s).off(t.eventTriggers).unwrap().unwrap(), t.state.enabled = !1)
            }
        }, e.fn[r] = function(t) {
            return this.each(function() {
                var n = e.data(this, r);
                n && !n.disableOnMobile ? typeof t == "string" && n[t] ? n[t]() : n.init(t) : e.data(this, r, new a(this, t))
            })
        }, e.fn[r].defaults = {
            onChange: function(t) {
                e(t).change()
            },
            maxHeight: 300,
            keySearchTimeout: 500,
            arrowButtonMarkup: '<b class="button">&#x25be;</b>',
            disableOnMobile: !1,
            nativeOnMobile: !0,
            openOnFocus: !0,
            openOnHover: !1,
            hoverIntentTimeout: 500,
            expandToItemText: !1,
            responsive: !1,
            preventWindowScroll: !0,
            inheritOriginalWidth: !1,
            allowWrap: !0,
            forceRenderAbove: !1,
            stopPropagation: !0,
            optionsItemBuilder: "{text}",
            labelBuilder: "{text}",
            listBuilder: !1,
            keys: {
                previous: [37, 38],
                next: [39, 40],
                select: [9, 13, 27],
                open: [13, 32, 37, 38, 39, 40],
                close: [9, 27]
            },
            customClass: {
                prefix: r,
                camelCase: !1
            },
            multiple: {
                separator: ", ",
                keepMenuOpen: !0,
                maxLabelEntries: !1
            }
        }
    }), "use strict",
    function(e, t, n) {
        typeof define == "function" && define.amd ? define(["jquery"], e) : typeof exports == "object" ? module.exports = e(require("jquery")) : e(t || n)
    }(function(e) {
        var t = function(t, n, r) {
            var i = {
                invalid: [],
                getCaret: function() {
                    try {
                        var e, n = 0,
                            r = t.get(0),
                            s = document.selection,
                            o = r.selectionStart;
                        if (s && navigator.appVersion.indexOf("MSIE 10") === -1) e = s.createRange(), e.moveStart("character", -i.val().length), n = e.text.length;
                        else if (o || o === "0") n = o;
                        return n
                    } catch (u) {}
                },
                setCaret: function(e) {
                    try {
                        if (t.is(":focus")) {
                            var n, r = t.get(0);
                            r.setSelectionRange ? r.setSelectionRange(e, e) : (n = r.createTextRange(), n.collapse(!0), n.moveEnd("character", e), n.moveStart("character", e), n.select())
                        }
                    } catch (i) {}
                },
                events: function() {
                    t.on("keydown.mask", function(e) {
                        t.data("mask-keycode", e.keyCode || e.which), t.data("mask-previus-value", t.val()), t.data("mask-previus-caret-pos", i.getCaret()), i.maskDigitPosMapOld = i.maskDigitPosMap
                    }).on(e.jMaskGlobals.useInput ? "input.mask" : "keyup.mask", i.behaviour).on("paste.mask drop.mask", function() {
                        setTimeout(function() {
                            t.keydown().keyup()
                        }, 100)
                    }).on("change.mask", function() {
                        t.data("changed", !0)
                    }).on("blur.mask", function() {
                        o !== i.val() && !t.data("changed") && t.trigger("change"), t.data("changed", !1)
                    }).on("blur.mask", function() {
                        o = i.val()
                    }).on("focus.mask", function(t) {
                        r.selectOnFocus === !0 && e(t.target).select(), e("#billing-info").find("label").removeClass("active"), e(this).closest("tr").find("label:not(#address_2_spaceholder)").addClass("active")
                    }).on("focusout.mask", function() {
                        r.clearIfNotMatch && !u.test(i.val()) && i.val("")
                    })
                },
                getRegexMask: function() {
                    var e = [],
                        t, r, i, o, u, a;
                    for (var f = 0; f < n.length; f++) t = s.translation[n.charAt(f)], t ? (r = t.pattern.toString().replace(/.{1}$|^.{1}/g, ""), i = t.optional, o = t.recursive, o ? (e.push(n.charAt(f)), u = {
                        digit: n.charAt(f),
                        pattern: r
                    }) : e.push(!i && !o ? r : r + "?")) : e.push(n.charAt(f).replace(/[-\/\\^$*+?.()|[\]{}]/g, "\\$&"));
                    return a = e.join(""), u && (a = a.replace(new RegExp("(" + u.digit + "(.*" + u.digit + ")?)"), "($1)?").replace(new RegExp(u.digit, "g"), u.pattern)), new RegExp(a)
                },
                destroyEvents: function() {
                    var e = ["input", "keydown", "keyup", "paste", "drop", "blur", "focusout", ""];
                    for (var n = 0; n < e.length; n++) t.off(e[n] = ".mask")
                },
                val: function(e) {
                    var n = t.is("input"),
                        r = n ? "val" : "text",
                        i;
                    return arguments.length > 0 ? (t[r]() !== e && t[r](e), i = t) : i = t[r](), i
                },
                calculateCaretPosition: function() {
                    var e = t.data("mask-previus-value") || "",
                        n = i.getMasked(),
                        r = i.getCaret();
                    if (e !== n) {
                        var s = t.data("mask-previus-caret-pos") || 0,
                            o = n.length,
                            u = e.length,
                            a = 0,
                            f = 0,
                            l = 0,
                            c = 0,
                            h = 0;
                        for (h = r; h < o; h++) {
                            if (!i.maskDigitPosMap[h]) break;
                            f++
                        }
                        for (h = r - 1; h >= 0; h--) {
                            if (!i.maskDigitPosMap[h]) break;
                            a++
                        }
                        for (h = r - 1; h >= 0; h--) i.maskDigitPosMap[h] && l++;
                        for (h = s - 1; h >= 0; h--) i.maskDigitPosMapOld[h] && c++;
                        if (r > u) r = o;
                        else if (s >= r && s !== u) {
                            if (!i.maskDigitPosMapOld[r]) {
                                var d = r;
                                r -= c - l, r -= a, i.maskDigitPosMap[r] && (r = d)
                            }
                        } else r > s && (r += l - c, r += f)
                    }
                    return r
                },
                behaviour: function(n) {
                    n = n || window.event, i.invalid = [];
                    var r = t.data("mask-keycode");
                    if (e.inArray(r, s.byPassKeys) === -1) {
                        var o = i.getMasked(),
                            u = i.getCaret();
                        return setTimeout(function() {
                            i.setCaret(i.calculateCaretPosition())
                        }, 10), i.val(o), i.setCaret(u), i.callbacks(n)
                    }
                },
                getMasked: function(e, t) {
                    var o = [],
                        u = t === undefined ? i.val() : t + "",
                        a = 0,
                        f = n.length,
                        l = 0,
                        c = u.length,
                        h = 1,
                        d = "push",
                        v = -1,
                        m = 0,
                        g = [],
                        y, b;
                    r.reverse ? (d = "unshift", h = -1, y = 0, a = f - 1, l = c - 1, b = function() {
                        return a > -1 && l > -1
                    }) : (y = f - 1, b = function() {
                        return a < f && l < c
                    });
                    var w;
                    while (b()) {
                        var E = n.charAt(a),
                            S = u.charAt(l),
                            x = s.translation[E];
                        x ? (S.match(x.pattern) ? (o[d](S), x.recursive && (v === -1 ? v = a : a === y && (a = v - h), y === v && (a -= h)), a += h) : S === w ? (m--, w = undefined) : x.optional ? (a += h, l -= h) : x.fallback ? (o[d](x.fallback), a += h, l -= h) : i.invalid.push({
                            p: l,
                            v: S,
                            e: x.pattern
                        }), l += h) : (e || o[d](E), S === E ? (g.push(l), l += h) : (w = E, g.push(l + m), m++), a += h)
                    }
                    var T = n.charAt(y);
                    f === c + 1 && !s.translation[T] && o.push(T);
                    var N = o.join("");
                    return i.mapMaskdigitPositions(N, g, c), N
                },
                mapMaskdigitPositions: function(e, t, n) {
                    var s = r.reverse ? e.length - n : 0;
                    i.maskDigitPosMap = {};
                    for (var o = 0; o < t.length; o++) i.maskDigitPosMap[t[o] + s] = 1
                },
                callbacks: function(e) {
                    var s = i.val(),
                        u = s !== o,
                        a = [s, e, t, r],
                        f = function(e, t, n) {
                            typeof r[e] == "function" && t && r[e].apply(this, n)
                        };
                    f("onChange", u === !0, a), f("onKeyPress", u === !0, a), f("onComplete", s.length === n.length, a), f("onInvalid", i.invalid.length > 0, [s, e, t, i.invalid, r])
                }
            };
            t = e(t);
            var s = this,
                o = i.val(),
                u;
            n = typeof n == "function" ? n(i.val(), undefined, t, r) : n, s.mask = n, s.options = r, s.remove = function() {
                var e = i.getCaret();
                return i.destroyEvents(), i.val(s.getCleanVal()), i.setCaret(e), t
            }, s.getCleanVal = function() {
                return i.getMasked(!0)
            }, s.getMaskedVal = function(e) {
                return i.getMasked(!1, e)
            }, s.init = function(o) {
                o = o || !1, r = r || {}, s.clearIfNotMatch = e.jMaskGlobals.clearIfNotMatch, s.byPassKeys = e.jMaskGlobals.byPassKeys, s.translation = e.extend({}, e.jMaskGlobals.translation, r.translation), s = e.extend(!0, {}, s, r), u = i.getRegexMask();
                if (o) i.events(), i.val(i.getMasked());
                else {
                    r.placeholder && t.attr("placeholder", r.placeholder), t.data("mask") && t.attr("autocomplete", "off");
                    for (var a = 0, f = !0; a < n.length; a++) {
                        var l = s.translation[n.charAt(a)];
                        if (l && l.recursive) {
                            f = !1;
                            break
                        }
                    }
                    f && t.attr("maxlength", n.length), i.destroyEvents(), i.events();
                    var c = i.getCaret();
                    i.val(i.getMasked()), i.setCaret(c)
                }
            }, s.init(!t.is("input"))
        };
        e.maskWatchers = {};
        var n = function() {
                var n = e(this),
                    i = {},
                    s = "data-mask-",
                    o = n.attr("data-mask");
                n.attr(s + "reverse") && (i.reverse = !0), n.attr(s + "clearifnotmatch") && (i.clearIfNotMatch = !0), n.attr(s + "selectonfocus") === "true" && (i.selectOnFocus = !0);
                if (r(n, o, i)) return n.data("mask", new t(this, o, i))
            },
            r = function(t, n, r) {
                r = r || {};
                var i = e(t).data("mask"),
                    s = JSON.stringify,
                    o = e(t).val() || e(t).text();
                try {
                    return typeof n == "function" && (n = n(o)), typeof i != "object" || s(i.options) !== s(r) || i.mask !== n
                } catch (u) {}
            },
            i = function(e) {
                var t = document.createElement("div"),
                    n;
                return e = "on" + e, n = e in t, n || (t.setAttribute(e, "return;"), n = typeof t[e] == "function"), t = null, n
            };
        e.fn.mask = function(n, i) {
            i = i || {};
            var s = this.selector,
                o = e.jMaskGlobals,
                u = o.watchInterval,
                a = i.watchInputs || o.watchInputs,
                f = function() {
                    if (r(this, n, i)) return e(this).data("mask", new t(this, n, i))
                };
            return e(this).each(f), s && s !== "" && a && (clearInterval(e.maskWatchers[s]), e.maskWatchers[s] = setInterval(function() {
                e(document).find(s).each(f)
            }, u)), this
        }, e.fn.masked = function(e) {
            return this.data("mask").getMaskedVal(e)
        }, e.fn.unmask = function() {
            return clearInterval(e.maskWatchers[this.selector]), delete e.maskWatchers[this.selector], this.each(function() {
                var t = e(this).data("mask");
                t && t.remove().removeData("mask")
            })
        }, e.fn.cleanVal = function() {
            return this.data("mask").getCleanVal()
        }, e.applyDataMask = function(t) {
            t = t || e.jMaskGlobals.maskElements;
            var r = t instanceof e ? t : e(t);
            r.filter(e.jMaskGlobals.dataMaskAttr).each(n)
        };
        var s = {
            maskElements: "input,td,span,div",
            dataMaskAttr: "*[data-mask]",
            dataMask: !0,
            watchInterval: 300,
            watchInputs: !0,
            useInput: !/Chrome\/[2-4][0-9]|SamsungBrowser/.test(window.navigator.userAgent) && i("input"),
            watchDataMask: !1,
            byPassKeys: [9, 16, 17, 18, 36, 37, 38, 39, 40, 91],
            translation: {
                0: {
                    pattern: /\d/
                },
                9: {
                    pattern: /\d/,
                    optional: !0
                },
                "#": {
                    pattern: /\d/,
                    recursive: !0
                },
                A: {
                    pattern: /[a-zA-Z0-9]/
                },
                S: {
                    pattern: /[a-zA-Z]/
                }
            }
        };
        e.jMaskGlobals = e.jMaskGlobals || {}, s = e.jMaskGlobals = e.extend(!0, {}, s, e.jMaskGlobals), s.dataMask && e.applyDataMask(), setInterval(function() {
            e.jMaskGlobals.watchDataMask && e.applyDataMask()
        }, s.watchInterval)
    }, window.jQuery, window.Zepto),
    function() {
        var e, t;
        String.prototype.titleize = function() {
            return this.charAt(0).toUpperCase() + this.slice(1)
        }, e = function(e) {
            return e["Product Color"] = $("p.style").text(), e["Product Cost"] = $("p.price").text().replace(/[^\d\.]/, ""), e.Currency = $("p.price span").data("currency"), e["Product Name"] = $("#details h1").text(), e.Category = $("#details h1").data("category"), e["Sold Out?"] = $(".button.sold-out").length > 0, e["Product Number"] = $("#details h1").data("ino"), e["Release Week"] = $("#details h1").data("rw"), e["Release Date"] = $("#details h1").data("rd"), e
        }, t = typeof exports != "undefined" && exports !== null ? exports : this, t.ga_track = function() {
            var t, n, r, i, s, o;
            n = [].slice.call(arguments), t = n.shift(), n[0] === "mp_only" && (o = !0, n.shift()), i = [], t.match(/ecommerce/) ? (typeof ga != "undefined" && ga !== null && ga("require", "ecommerce", "ecommerce.js"), typeof ga_eu != "undefined" && ga_eu !== null && ga_eu("require", "ecommerce", "ecommerce.js"), i = [t]) : i = ["send", t], typeof ga != "undefined" && ga !== null && !o && ga.apply(ga, i.concat(n)), typeof ga_eu != "undefined" && ga_eu !== null && !o && ga_eu.apply(ga, i.concat(n)), typeof mixpanel != "undefined" && mixpanel !== null && (s = {
                URL: location.pathname,
                "Page Name": document.title.replace("Supreme: ", ""),
                Season: "FW17",
                "Device Type": "Desktop",
                "Event Name": t
            }, s["Site Region"] = $("body").hasClass("us") ? "US" : $("body").hasClass("eu") ? "EU" : "JP", t === "Add to Cart Attempt" && (s = e(s), s["Product Size"] = "Medium", mixpanel.track("Add to Cart Attempt", s)), t === "Purchase Attempt" && $.each(n[0], function(e, t) {
                return mixpanel.track("Purchase Attempt", $.extend(s, t))
            }), (t === "Purchase Success" || t === "Checkout View") && mixpanel.track(t, $.extend(s, n[0])), t === "pageview" && (location.pathname.match(/^\/shop\/?$/) || location.pathname.match(/^\/shop\/all/) ? (location.pathname.match(/shop\/all\/[a-z]/i) ? s["Shop View Type"] = location.pathname.split("/").slice(-1)[0].titleize() : location.pathname.match(/shop\/all/i) ? s["Shop View Type"] = "All" : s["Shop View Type"] = "Tile", r = "Shop View") : location.pathname.match(/^\/shop/) && location.pathname.split("/").length > 3 ? (r = "Product View", s = e(s)) : !(location.pathname.match(/^\/preview/) && location.pathname.split("/").length > 3) && !location.pathname.match(/^\/shop/) && !location.pathname.match(/^\/checkout/) && (r = "Menu Page View"), s["Event Name"] = r, mixpanel.track(r, s)));
            if (typeof _gaq != "undefined" && _gaq !== null && !o) return t = t.replace("ecommerce:send", "trans"), t = t.replace("ecommerce:", ""), t = t.replace("addTransaction", "addTrans"), t.match(/^add/) ? t = "_" + t : t = "_track" + t.titleize(), _gaq.push([t].concat(n))
        }
    }.call(this), $(document).ready(function() {
        Cart = Backbone.Model.extend({
            url: "/shop/cart.json",
            defaults: {
                sizes: null,
                order_billing_country: ""
            },
            initialize: function() {
                var e = new StyleSizes;
                this.set("sizes", e);
                var t = this
            },
            hasStyle: function(e) {
                var t = this.get("sizes").find(function(t) {
                    return t.get("style").get("id") == e.get("id")
                });
                return !_.isUndefined(t)
            },
            hasProduct: function(e) {
                var t = this.get("sizes").find(function(t) {
                    var n = e.get("styles").map(function(e) {
                        return e.get("id")
                    });
                    return _.include(n, t.get("style").get("id"))
                });
                return !_.isUndefined(t)
            },
            render: function() {},
            hasItemsPreventingFreeShipping: function() {
                var e = !1;
                for (var t = 0; t < localStorage.length; t++) {
                    var n = localStorage.key(t);
                    if (!storageKeyIsProduct(n)) continue;
                    this.getSizeFromLocalStorage(n).get("no_free_shipping") && (e = !0)
                }
                return e
            },
            hasItemsPreventingCOD: function() {
                var e = !1;
                for (var t = 0; t < localStorage.length; t++) {
                    var n = localStorage.key(t);
                    if (!storageKeyIsProduct(n)) continue;
                    this.getSizeFromLocalStorage(n).get("cod_blocked") && (e = !0)
                }
                return e
            },
            hasItemsPreventingCanada: function() {
                var e = !1;
                for (var t = 0; t < localStorage.length; t++) {
                    var n = localStorage.key(t);
                    if (!storageKeyIsProduct(n)) continue;
                    this.getSizeFromLocalStorage(n).get("canada_blocked") && (e = !0)
                }
                return e
            },
            hasItemsPreventingRussia: function() {
                var e = !1;
                for (var t = 0; t < localStorage.length; t++) {
                    var n = localStorage.key(t);
                    if (!storageKeyIsProduct(n)) continue;
                    this.getSizeFromLocalStorage(n).get("russia_blocked") && (e = !0)
                }
                return e
            },
            hasItemsPreventingNonEU: function() {
                var e = !1;
                for (var t = 0; t < localStorage.length; t++) {
                    var n = localStorage.key(t);
                    if (!storageKeyIsProduct(n)) continue;
                    this.getSizeFromLocalStorage(n).get("non_eu_blocked") && (e = !0)
                }
                return e
            },
            itemTotal: function() {
                var e = 0;
                for (var t = 0; t < localStorage.length; t++) {
                    var n = localStorage.key(t);
                    if (!storageKeyIsProduct(n)) continue;
                    var r = this.getSizeFromLocalStorage(n);
                    e += r.get("style").get("product").actualPrice(r.get("qty"))
                }
                return e
            },
            canAddStyle: function(e) {
                var t = this;
                if (e.get("product").get("can_add_styles")) {
                    if (this.hasStyle(e)) return !1;
                    var n = e.get("product").get("can_buy_multiple_with_limit");
                    if (n > 1) {
                        var r = 0;
                        return _.each(e.get("product").get("styles").models, function(e) {
                            t.hasStyle(e) && r++
                        }), r < n
                    }
                    return !0
                }
                return this.hasProduct(e.get("product")) ? !1 : !0
            },
            getServerContents: function(e) {
                var t = this,
                    n = "/shop/cart_extended.json";
                $.ajax({
                    type: "GET",
                    url: n,
                    dataType: "json",
                    success: function(t) {
                        e(t)
                    },
                    error: function() {}
                })
            },
            changeQty: function(e, t, n) {
                var r = this,
                    i = "/shop/" + e + "/update_qty.json";
                $.ajax({
                    type: "DELETE",
                    url: i,
                    data: {
                        size: t,
                        qty: n
                    },
                    dataType: "json",
                    success: function(e) {
                        localStorage.setItem(t + "_qty", n), r.trigger("itemAdded"), r.trigger("doneModifyingCart")
                    },
                    error: function() {
                        r.trigger("doneModifyingCart")
                    }
                })
            },
            removeSize: function(e, t) {
                var n = this,
                    r = "/shop/" + e + "/remove.json";
                $.ajax({
                    type: "DELETE",
                    url: r,
                    data: {
                        size: t
                    },
                    dataType: "json",
                    success: function(e) {
                        var r = n.get("sizes").find(function(e) {
                            return e.get("id") == parseInt(t)
                        });
                        n.get("sizes").remove(r), n.removeSizeFromLocalStorage(t), n.trigger("itemAdded"), Supreme.app.cart.length() == 0 && clearCookies(), n.trigger("doneModifyingCart")
                    },
                    error: function() {
                        n.trigger("doneModifyingCart")
                    }
                })
            },
            removeProduct: function(e, t) {
                this.removeSize(e.get("id"), t)
            },
            length: function() {
                var e = 0;
                for (var t = 0; t < localStorage.length; t++) {
                    var n = localStorage.key(t);
                    if (!storageKeyIsProduct(n)) continue;
                    e++
                }
                return e
            },
            addSize: function(e, t, n) {
                var r = this;
                t = t || 1;
                if (!this.canAddStyle(e.get("style"))) return this.trigger("doubleItemError"), !1;
                try {
                    var r = this;
                    this.get("sizes").add(e, t);
                    var i = "/shop/" + e.get("style").get("product").get("id") + "/add.json";
                    $.ajax({
                        type: "POST",
                        url: i,
                        data: {
                            size: e.get("id"),
                            style: e.get("style").get("id"),
                            qty: t
                        },
                        dataType: "json",
                        success: function(n) {
                            var i = _.find(n, function(t) {
                                    return e.get("id") == t.size_id
                                }),
                                s;
                            _.isUndefined(i) ? (s = !0, Supreme.app.cart.removeSizeDirectly(e.get("id")), r.trigger("itemSoldOut")) : (s = !1, r.addSizeToLocalStorage(e, t), r.trigger("itemAdded"));
                            try {
                                var o;
                                IS_JAPAN ? o = "JPY" : IS_EU ? o = LANG.currency == "eur" ? "EUR" : "GBP" : o = "USD";
                                var u = formatCurrency(e.get("style").get("product").actualPrice()).replace(/[^\d\.]/, "");
                                mixpanelTrack("Add to Cart Attempt", {
                                    Category: _currentCategoryPlural,
                                    "Product Color": e.get("style").get("name"),
                                    "Product Name": e.get("style").get("product").get("name"),
                                    "Product Size": e.get("name"),
                                    "Product Cost": u,
                                    Currency: o,
                                    "Device Type": "Mobile",
                                    "Sold Out?": s
                                })
                            } catch (a) {
                                trackJs.track(a)
                            }
                            r.trigger("doneModifyingCart")
                        },
                        error: function() {
                            r.trigger("doneModifyingCart")
                        }
                    })
                } catch (s) {
                    this.trigger("doubleItemError"), this.trigger("doneModifyingCart")
                }
            },
            getSizeFromLocalStorage: function(e) {
                var t = JSON.parse(localStorage.getItem(e)),
                    n = new ProductStyle(t.style),
                    r = new Product(Supreme.getProductOverviewDetailsForId(n.get("product_id"), allCategoriesAndProducts));
                r.set("apparel", t.apparel), r.set("handling", t.handling), n.set("product", r), r.set("can_buy_multiple", t.can_buy_multiple), r.set("purchasable_qty", t.purchasable_qty);
                var i = new StyleSize({
                    id: t.id,
                    name: t.name,
                    stock_level: t.stock_level,
                    style: n,
                    no_free_shipping: t.no_free_shipping,
                    cod_blocked: t.cod_blocked,
                    canada_blocked: t.canada_blocked,
                    non_eu_blocked: t.non_eu_blocked,
                    russia_blocked: t.russia_blocked,
                    qty: localStorage.getItem(e + "_qty")
                });
                return i
            },
            removeSizeDirectly: function(e) {
                var t = this.get("sizes").find(function(t) {
                    return t.get("id") == parseInt(e)
                });
                this.get("sizes").remove(t), this.removeSizeFromLocalStorage(e), this.trigger("itemAdded")
            },
            addSizeToLocalStorage: function(e, t) {
                localStorage.setItem(e.get("id"), JSON.stringify(e.toDeepJSON())), this.getSizeFromLocalStorage(e.get("id")), localStorage.setItem(e.get("id") + "_qty", t)
            },
            removeSizeFromLocalStorage: function(e) {
                localStorage.removeItem(e), localStorage.removeItem(e + "_qty")
            },
            soldOutSizesFromSizeIds: function(e) {
                var t = new StyleSizes;
                return this.get("sizes").each(function(n) {
                    var r = n.get("id");
                    _.include(e, r) || t.add(n)
                }), t
            },
            updateFromCookie: function(e) {
                this.get("sizes").reset();
                var t = e.split("--"),
                    n = t[2].split("-"),
                    r = this;
                _.each(n, function(e) {
                    var t = parseInt(e.split(",")[0]),
                        n = parseInt(e.split(",")[1]),
                        i = new ProductStyle({
                            id: n
                        }),
                        s = new StyleSize({
                            id: t,
                            style: i
                        });
                    r.get("sizes").add(s)
                })
            },
            quantityForSize: function(e) {
                var t = this.get("sizes").find(function(t) {
                    return t.get("id") == parseInt(e)
                });
                if (t) return t.get("qty")
            },
            getSizeForStyle: function(e) {
                var t = this.get("sizes").find(function(t) {
                    return t.get("style").get("id") == e.get("id")
                });
                return _.isUndefined(t) ? !1 : t
            },
            parse: function(e) {
                _.each(e, function(e) {
                    e.in_stock ? sessionStorage.removeItem("out_of_stock_" + e.size_id) : sessionStorage.setItem("out_of_stock_" + e.size_id, 1)
                });
                var t = this;
                for (var n = 0; n < localStorage.length; n++) {
                    var r = localStorage.key(n);
                    if (!storageKeyIsProduct(r)) continue;
                    var i = _.find(e, function(e) {
                        return e.size_id == r
                    });
                    _.isUndefined(i) && t.removeSizeDirectly(r)
                }
            },
            otherItemsTotal: function() {
                var e = this.get("sizes").filter(function(e) {
                    return !e.get("style").get("product").get("apparel")
                });
                return e.reduce(function(e, t) {
                    return e + t.get("style").get("product").actualPrice()
                }, 0)
            },
            shippingTotal: function(e, t) {
                if (IS_JAPAN) return FREE_SHIPPING && !this.hasItemsPreventingFreeShipping() && this.itemTotal() >= MIN_JP_FREE ? 0 : SHIPPING_RATE + this.handlingTotal();
                if (IS_EU) {
                    var n = COUNTRIES[e],
                        r = this.shipping_rate_for_country(e);
                    if (FREE_SHIPPING && !this.hasItemsPreventingFreeShipping()) {
                        var i = this.itemTotal();
                        if (e == "GB" && i > MIN_UK_FREE) return 0;
                        if (this.hasVat(e) && i > MIN_EU_FREE) return 0;
                        if (!this.hasVat(e) && i > MIN_NON_EU_FREE) return 0
                    }
                    return r
                }
                var s = 0;
                if (FREE_SHIPPING && !this.hasItemsPreventingFreeShipping()) {
                    var i = this.itemTotal();
                    if (e.toLowerCase() != "canada" && i > MIN_US_FREE) return 0
                }
                return e.toLowerCase() == "canada" ? s = shippingRates.canada : _.include(["AK", "HI", "GU", "PR"], t.toUpperCase()) ? s = shippingRates.spec : s = shippingRates.std, s + this.handlingTotal()
            },
            handlingTotal: function() {
                return this.get("sizes").reduce(function(e, t) {
                    return e + t.get("style").get("product").get("handling")
                }, 0)
            },
            hasVat: function(e) {
                return IS_EU ? !_.contains(NON_VAT_COUNTRIES, e) : 0
            },
            vatDiscount: function(e, t) {
                if (!IS_EU) return 0;
                if (this.hasVat(e)) return 0;
                var n = 1.2,
                    r = this.itemTotal(),
                    i = r - r / n,
                    s = this.shippingTotal(e, t),
                    o = s - s / n;
                return i + o
            },
            codTotal: function(e) {
                if (!IS_JAPAN) return 0;
                if (e != "cod") return 0;
                cod_chargable_total = this.itemTotal() + this.shippingTotal();
                var t = _.detect(COD_RATES, function(e) {
                    threshold = e[0].fractional, rate = e[1].fractional;
                    if (cod_chargable_total <= threshold) return rate
                });
                return t[1].fractional
            },
            taxTotal: function(e, t) {
                if (!IS_US) return 0;
                if (t.toUpperCase() == "NY") {
                    var n = (this.otherItemsTotal() + this.shippingTotal(e, t)) * TAX_RATE + this.apparelTaxTotal();
                    return Math.round(n)
                }
                return 0
            },
            apparelTaxTotal: function() {
                var e = 0,
                    t = this,
                    n = this.get("sizes").select(function(e) {
                        return e.get("style").get("product").get("apparel") && e.get("style").get("product").actualPrice() >= APPAREL_THRESHOLD
                    });
                if (n.length > 0) {
                    var r = n.reduce(function(e, t) {
                        return e + t.get("style").get("product").actualPrice()
                    }, 0);
                    e += r * APPAREL_TAX_RATE
                }
                return e
            },
            orderTotal: function(e, t, n) {
                var r = this.itemTotal() + this.taxTotal(t, n) + this.shippingTotal(t, n),
                    i = this.codTotal(e);
                return i > 0 && (r = parseInt(r) + parseInt(i)), IS_EU && (r -= this.vatDiscount(t)), r
            },
            shipping_rate_for_country: function(e) {
                var t = _.detect(COUNTRIES, function(t) {
                    return t.ups_cc == e
                });
                if (!t) return DEFAULT_SHIPPING_RATE;
                var n = ZONES[t.zone];
                return n
            }
        })
    }), $(document).ready(function() {
        Category = Backbone.Model.extend({
            defaults: {
                name: null,
                products: null
            }
        })
    }), $(document).ready(function() {
        CheckoutForm = Backbone.Model.extend({
            defaults: {
                order_billing_name: null,
                order_email: null,
                order_tel: null,
                order_billing_address: null,
                order_billing_city: null,
                order_billing_zip: null,
                same_as_billing_address: null,
                order_billing_state: "",
                order_billing_country: IS_JAPAN ? "JAPAN" : IS_EU ? "GB" : "USA",
                credit_card_type: "visa"
            },
            processFormResponse: function(e) {
                Supreme.app.cart.lastError = "";
                if (e.status == "failed") return _.isUndefined(e.errors) ? (this.trigger("chargeFailed", e.b), logError("jsonResponse.errors undefined in processFormResponse", e)) : (this.processErrorFormResponse(e.errors), mixpanelTrack("Purchase Attempt", e.mpa)), !0;
                if (e.status == "paid") return this.processSuccessfulFormResponse(e.info), mixpanelTrack("Purchase Attempt", e.mpa), mixpanelTrack("Purchase Success", e.mps), mixpanel && mixpanel.register({
                    "Repeat Customer": !0
                }), !0;
                if (e.status == "dup") return this.processDupFormResponse(), !1;
                if (e.status == "canada") return this.processCanadaFormResponse(), !1;
                if (e.status == "blocked_country") return this.processBlockedCountryFormResponse(), !1;
                if (e.status == "blacklisted") return this.processBlackListedFormResponse(), !1;
                if (e.status == "outOfStock") return this.processOutOfStockFormResponse(), mixpanelTrack("Purchase Attempt", e.mpa), !1;
                e.status == "paypal" ? (window.location.href = e.redirect_url, _.isNull(readCookie("remember_me")) && eraseCookie("js-address")) : (this.trigger("chargeFailed"), logError("else error in processFormResponse", e))
            },
            processOutOfStockFormResponse: function() {
                Supreme.app.cart.lastError = "outOfStock", this.trigger("outOfStockError")
            },
            processDupFormResponse: function() {
                Supreme.app.cart.lastError = "dup", this.trigger("dupError")
            },
            processCanadaFormResponse: function() {
                Supreme.app.cart.lastError = "canada", this.trigger("dupError")
            },
            processBlockedCountryFormResponse: function() {
                Supreme.app.cart.lastError = "blocked_country", this.trigger("dupError")
            },
            processBlackListedFormResponse: function() {
                Supreme.app.cart.lastError = "blacklisted", this.trigger("dupError")
            },
            processSuccessfulFormResponse: function(e) {
                clearCookies(), _.isNull(readCookie("remember_me")) && eraseCookie("js-address");
                var t = new OrderConfirmationView({
                    el: $("#main")[0],
                    model: e
                });
                t.render(), localStorage.clear(), $("#cart-link").remove(), Supreme.app.cart = new Cart, eraseCookie("cart"), Supreme.app.cartLinkView = new CartLinkView({
                    model: Supreme.app.cart
                }), $("header").prepend(Supreme.app.cartLinkView.render().el), $("html").addClass("orderConfirm")
            },
            processErrorFormResponse: function(e) {
                this.trigger("checkoutErrors", e)
            },
            pollCardStatus: function(e) {
                var t = this;
                $.ajax({
                    type: "POST",
                    url: "/check_order_status.json",
                    dataType: "json",
                    success: function(e) {
                        e.status == "failed" && t.trigger("chargeFailed")
                    },
                    error: function(e, t) {}
                })
            }
        })
    }), $(document).ready(function() {
        LookbookItem = Backbone.Model.extend({
            defaults: {
                caption: null,
                medium: null,
                name: null,
                products: null,
                small: null,
                title: null,
                url: null
            },
            stripSomeMarkupFromCaption: function() {
                var e = this.get("caption").replace(/<a.*?>/ig, "").replace(/<\/a>/ig, "");
                this.set("caption", e)
            }
        })
    }), $(document).ready(function() {
        Product = Backbone.Model.extend({
            url: function() {
                return "/shop/" + this.get("id") + ".json"
            },
            initialize: function() {
                var e = this;
                this.bind("change:styles", function() {
                    var t = _.first(e.get("styles"))
                })
            },
            defaults: {
                purchasable_qty: 0,
                categoryName: null,
                styles: null,
                initialImageUrl: null,
                name: null,
                price: null,
                price_euro: null,
                sale_price: null,
                sale_price_euro: null,
                selectedStyle: null,
                can_add_styles: !1,
                can_buy_multiple: !1,
                can_buy_multiple_with_limit: 0,
                no_free_shipping: !1,
                selectedStyleAlt: 0,
                qty: 0
            },
            showAlt: function(e) {
                this.set("photoIndex", e)
            },
            actualPrice: function(e) {
                return _.isUndefined(e) ? e = 1 : e = parseInt(e), this.get("sale_price") != 0 && SALE_VISIBLE ? LANG.currency == "eur" ? this.get("sale_price_euro") * e : this.get("sale_price") * e : LANG.currency == "eur" ? this.get("price_euro") * e : this.get("price") * e
            },
            isOnSale: function() {
                return this.get("sale_price") != 0 && SALE_VISIBLE
            },
            sizeForId: function(e) {
                var t;
                return this.get("styles").each(function(n) {
                    var r = n.get("sizes").find(function(t) {
                        return t.get("id") == e
                    });
                    if (!_.isUndefined(r)) {
                        t = r;
                        return
                    }
                }), t
            },
            parse: function(e, t) {
                var n = !1;
                typeof window.splayver == "undefined" ? t.getResponseHeader("X-Splay-Version") != null && t.getResponseHeader("X-Splay-Version").match(/^[0-9a-f]{16}$/) != null && t.getResponseHeader("X-Splay-Version").match(/^[0-9a-f]{16}$/).length == 1 && t.getResponseHeader("X-Splay-Version").match(/^[0-9a-f]{16}$/)[0] == t.getResponseHeader("X-Splay-Version") && (window.splayver = t.getResponseHeader("X-Splay-Version")) : t.getResponseHeader("X-Splay-Version") != null && t.getResponseHeader("X-Splay-Version") != window.splayver && t.getResponseHeader("X-Splay-Version").match(/^[0-9a-f]{16}$/) != null && t.getResponseHeader("X-Splay-Version").match(/^[0-9a-f]{16}$/).length == 1 && t.getResponseHeader("X-Splay-Version").match(/^[0-9a-f]{16}$/)[0] == t.getResponseHeader("X-Splay-Version") && (window.splayver = t.getResponseHeader("X-Splay-Version"), n = !0, location.reload());
                if (!n) {
                    var r = this,
                        i = new ProductStyles;
                    return this.set("styles", i), this.set("description", e.description), this.set("handling", e.handling), this.set("apparel", e.apparel), this.set("can_add_styles", e.can_add_styles), this.set("can_buy_multiple", e.can_buy_multiple), this.set("purchasable_qty", e.purchasable_qty), this.set("can_buy_multiple_with_limit", e.can_buy_multiple_with_limit), this.set("ino", e.ino), this.set("cod_blocked", e.cod_blocked), this.set("canada_blocked", e.canada_blocked), this.set("russia_blocked", e.russia_blocked), this.set("non_eu_blocked", e.non_eu_blocked), _.each(e.styles, function(t) {
                        isHiRes() && _.each(t.additional, function(e, n) {
                            t.additional[n].image_url = t.additional[n].image_url_hi, t.additional[n].swatch_url = t.additional[n].swatch_url_hi, t.additional[n].zoomed_url = t.additional[n].zoomed_url_hi, t.additional[n].lower_res_zoom = t.additional[n].image_url
                        });
                        var n = new ProductStyle({
                            id: t.id,
                            name: t.name,
                            price: t.price,
                            description: t.description,
                            swatch_url: isHiRes() ? t.swatch_url_hi : t.swatch_url,
                            zoomed_url: isHiRes() ? t.mobile_zoomed_url_hi : t.mobile_zoomed_url,
                            image_url: isHiRes() ? t.image_url_hi : t.image_url,
                            additional_images: t.additional,
                            lower_res_zoom: t.image_url
                        });
                        n.set("sizes", new StyleSizes), n.set("product", r), _.each(t.sizes, function(t) {
                            var r = new StyleSize(t);
                            r.set("no_free_shipping", e.no_free_shipping), r.set("cod_blocked", e.cod_blocked), r.set("canada_blocked", e.canada_blocked), r.set("russia_blocked", e.russia_blocked), r.set("non_eu_blocked", e.non_eu_blocked), r.set("style", n), n.get("sizes").add(r)
                        }), r.get("styles").add(n)
                    }), {}
                }
            }
        })
    }), $(document).ready(function() {
        ProductPreview = Backbone.Model.extend({
            defaults: {
                id: null,
                name: null,
                price: null,
                sale_price: null,
                image_url: null,
                image_url_hi: null
            }
        })
    }), $(document).ready(function() {
        ProductStyle = Backbone.Model.extend({
            defaults: {
                name: null,
                id: null,
                image_url: null,
                sizes: null,
                initialPhotoIndex: 0
            },
            isSoldOut: function() {
                return this.get("sizes").length == 0 ? !0 : this.get("sizes").all(function(e) {
                    return parseInt(e.get("stock_level")) == 0
                }) ? !0 : !1
            },
            productPhoto: function() {
                var e;
                return _.isUndefined(this.get("initialPhotoIndex")) ? e = this.get("product").get("selectedStyleAlt") : e = this.get("initialPhotoIndex"), e > 0 ? this.get("additional_images")[e - 1].image_url : this.get("image_url")
            },
            zoomedPhoto: function(e) {
                _.isUndefined(e) && (e = !1);
                var t;
                return _.isUndefined(this.get("initialPhotoIndex")) ? t = this.get("product").get("selectedStyleAlt") : t = this.get("initialPhotoIndex"), t > 0 ? e ? (console.log("using useLowerResImage"), this.get("additional_images")[t - 1].lower_res_zoom) : this.get("additional_images")[t - 1].zoomed_url : e ? (console.log("using useLowerResImage"), this.get("lower_res_zoom")) : this.get("zoomed_url")
            }
        })
    }), $(document).ready(function() {
        StyleSize = Backbone.Model.extend({
            defaults: {
                stock_level: null,
                name: null,
                id: null,
                qty: 1
            },
            toDeepJSON: function() {
                var e = this.toJSON();
                e.apparel = this.get("style").get("product").get("apparel"), e.handling = this.get("style").get("product").get("handling"), e.can_buy_multiple = this.get("style").get("product").get("can_buy_multiple"), e.purchasable_qty = this.get("style").get("product").get("purchasable_qty");
                var t = this.get("style").clone();
                return t.set("product_id", this.get("style").get("product").get("id")), t.set("sizes", null), t.set("product", null), e.style = t.toJSON(), e
            }
        })
    }), $(document).ready(function() {
        Categories = Backbone.Collection.extend({
            model: Category,
            populate: function(e) {
                var t = this;
                _.each(e.products_and_categories, function(e, n) {
                    var r = [];
                    _.each(e, function(e) {
                        var t = isHiRes() ? e.image_url_hi : e.image_url;
                        r.push(new Product({
                            id: e.id,
                            name: e.name,
                            image_url: t,
                            price: e.price,
                            sale_price: e.sale_price,
                            price_euro: e.price_euro,
                            sale_price_euro: e.sale_price_euro,
                            new_item: e.new_item,
                            position: e.position
                        }))
                    });
                    var i = new CategoryProducts(r),
                        s = new Category({
                            name: n,
                            products: i
                        });
                    t.add(s)
                })
            }
        })
    }), $(document).ready(function() {
        CategoryProducts = Backbone.Collection.extend({
            model: ProductPreview
        })
    }), $(document).ready(function() {
        Lookbook = Backbone.Collection.extend({
            model: LookbookItem,
            url: "/lookbooks.json",
            parse: function(e) {
                return this.title = e.title, e.images
            }
        })
    }), $(document).ready(function() {
        ProductStyles = Backbone.Collection.extend({
            model: ProductStyle
        })
    }), $(document).ready(function() {
        StyleSizes = Backbone.Collection.extend({
            model: StyleSize
        })
    }), $(document).ready(function() {
        AddToCartButtonView = Backbone.View.extend({
            tagName: "span",
            className: "cart-button",
            initialize: function() {
                this.render = _.bind(this.render, this), this.itemSoldOut = _.bind(this.itemSoldOut, this), Supreme.app.cart.bind("itemSoldOut", this.itemSoldOut), Supreme.app.cart.bind("doneModifyingCart", this.doneModifyingCart), this.isLoading = !1
            },
            doneModifyingCart: function() {
                this.isLoading = !1
            },
            soldOut: function() {
                this.mode = "sold-out"
            },
            itemSoldOut: function() {
                this.soldOut(), this.render()
            },
            render: function() {
                var e = Supreme.app.cart.hasStyle(this.model.get("selectedStyle")),
                    t = Supreme.app.cart.canAddStyle(this.model.get("selectedStyle"));
                return t ? this.mode == "sold-out" ? ($(this.el).text("sold out"), $(this.el).addClass("sold-out")) : ($(this.el).text(IS_JAPAN ? "ăŤăźăăŤĺĽăă" : LANG.addToCart), this.mode = "adding") : e && ($(this.el).addClass("delete"), $(this.el).text(LANG.removeFromCart), this.mode = "removing", $("#cart-warning").remove(), $("#widgets-container").show()), this
            },
            events: {
                click: "updateCart"
            },
            updateCart: function() {
                if (this.isLoading) return;
                this.isLoading = !0, this.mode == "adding" ? ($(this.el).text("adding").addClass("adding"), this.addToCart(), $.scrollTo(0)) : this.mode == "removing" && (IS_US ? $(this.el).text("removing") : $(this.el).text("..."), this.removeFromCart())
            },
            removeFromCart: function() {
                var e = $('select[name="size-options"]'),
                    t = e.val();
                Supreme.app.cart.removeProduct(this.model, t)
            },
            addToCart: function() {
                var e = $('select[name="size-options"]'),
                    t = e.val(),
                    n = $('select[name="qty"]').length ? +$('select[name="qty"]').val() : 1;
                if (_.isEmpty(t)) alert("Please select a size");
                else {
                    var r = this.model.sizeForId(t);
                    Supreme.app.cart.addSize(r, n), $("#cart-warning").remove()
                }
            }
        })
    }), $(document).ready(function() {
        CartItemView = Backbone.View.extend({
            tagName: "tr",
            template: _.template($("#cartItemViewTemplate").html()),
            events: {
                "click td.delete": "remove",
                "click td.cart-image img": "jumpToProduct",
                "click td.desc": "jumpToProduct",
                "change .cart-qty": "changeQty"
            },
            jumpToProduct: function() {
                _currentCategory = singularCategoryName(this.model.category), _currentCategoryPlural = this.model.category, Supreme.app.navigate("products/" + this.model.product_id + "/" + this.model.style_id, {
                    trigger: !0
                })
            },
            render: function() {
                return $(this.el).html($(this.template(this.model))), this
            },
            changeQty: function(e) {
                var t = $(e.target).val();
                Supreme.app.cart.changeQty(this.model.product_id, this.model.size_id, t)
            },
            remove: function() {
                Supreme.app.cart.removeSize(this.model.product_id, this.model.size_id);
                var e = this;
                $(this.el).animate({
                    opacity: 0
                }, fadeSpeed, fadeEasingType, function() {
                    $(e.el).remove()
                })
            }
        })
    }), $(document).ready(function() {
        CartLinkView = Backbone.View.extend({
            tagName: "div",
            id: "cart-link",
            initialize: function() {
                _.bindAll(this, "render"), this.model.view = this, this.model.bind("itemAdded", this.render), this.model.bind("doubleItemError", this.handleDoubleItemError)
            },
            render: function() {
                Supreme.app.cart.length() == 0 ? $(this.el).hide() : $(this.el).show();
                if (IS_JAPAN) {
                    var e = '<a href="#cart" id="goto-cart-link">' + Supreme.app.cart.length() + "</a>";
                    e += '<a href="#checkout" id="checkout-now"><span>ăćł¨ććçśăă¸</span></a>'
                } else {
                    var t = LANG.multiple_items;
                    Supreme.app.cart.length() == 1 && (t = LANG.single_item);
                    var e = '<a href="#cart" id="goto-cart-link">' + Supreme.app.cart.length() + "</a>";
                    e += '<a href="#checkout" id="checkout-now"><span>' + LANG.checkout + "</span></a>"
                }
                return $(this.el).html(e), this
            },
            handleDoubleItemError: function() {
                alert("You already have added this item to the cart.")
            },
            remove: function() {
                $(this.el).remove()
            }
        })
    }), $(document).ready(function() {
        CartView = Backbone.View.extend({
            template: _.template($("#cartViewTemplate").html()),
            el: $("#main")[0],
            initialize: function() {
                this.render = _.bind(this.render, this), Supreme.app.cart.bind("itemAdded", this.render)
            },
            render: function(e) {
                if (Backbone.history.fragment != "cart") return;
                $(this.el).empty();
                var t = this;
                $(this.el).html(this.template(this.model.toJSON())), $(".from-sold-out-items", this.el).remove(), Supreme.app.cart.lastError == "dup" ? $("table", this.el).before($('<div id="error">You have previously ordered this item(s). There is a limit of 1 style per product.</div>')) : Supreme.app.cart.lastError == "blacklisted" ? IS_JAPAN ? $("table", this.el).before($('<div id="error">ĺŠç¨čŚç´ăŤĺžăăžăăŚăăĺŽ˘ć§ăăăŽăćł¨ćăŻăć­ăăăăŚé ăăžăă</div>')) : $("table", this.el).before($('<div id="error">You have previously ordered this item(s). There is a limit of 1 style per product.</div>')) : Supreme.app.cart.lastError == "canada" ? $("table", this.el).before($('<div id="error">Some items in your cart are sold only in the U.S. (no Canada shipping).</div>')) : Supreme.app.cart.lastError == "blocked_country" ? $("table", this.el).before($('<div id="error">Some items in your cart are sold only in the EU.</div>')) : Supreme.app.cart.lastError == "outOfStock" ? $("table", this.el).before($('<div id="error">' + soldOutMessage + "</div>")) : $("#error", this.el).remove();
                var n = 0,
                    r = 0,
                    i = 0;
                Supreme.app.cart.getServerContents(function(e) {
                    $("table tbody", this.el).empty();
                    for (var t in e)
                        if (e.hasOwnProperty(t)) {
                            console.log(t, e[t]);
                            var n = new CartItemView({
                                model: e[t]
                            });
                            $("table tbody", this.el).append(n.render().el), i++
                        } $("#cart_item_count").text(i), i == 1 ? $("cart_item_count_singular").show() : $("cart_item_count_multiple").show()
                }), n > 0 && Supreme.app.cart.lastError != "outOfStock" && $("table", this.el).before($('<div id="error" class="from-sold-out-items">' + soldOutMessage + "</div>")), Supreme.app.cart.length() == 0 && _.delay(function() {
                    Supreme.app.navigate("#", {
                        trigger: !0
                    })
                }, 500), this.setSubtotal()
            },
            setSubtotal: function() {
                var e = Supreme.app.cart.itemTotal();
                $("#cart-total span.cart-subtotal", this.el).text(formatCurrency(e)), IS_EU && $("#cart-total span.cart-subtotal-euro", this.el).text(formatCurrency(GBPtoEuro(e), "âŹ", !0))
            }
        })
    }), $(document).ready(function() {
        CategoryCollectionView = Backbone.View.extend({
            template: _.template(categoryListTemplate),
            initialize: function() {
                var e = this;
                this._categoryListViews = [];
                var t = this.collection.sortBy(function(e) {
                    return _.indexOf(categoryOrder, e.get("name"))
                });
                _.each(t, function(t) {
                    e._categoryListViews.push(new CategoryListView({
                        model: t,
                        id: t.get("name").toLowerCase() + "-category"
                    }))
                })
            },
            render: function() {
                var e = this;
                e.updateContent()
            },
            updateContent: function() {
                var e = this;
                $(e.el).html(e.template);
                var t = $("<ul></ul>");
                _(e._categoryListViews).each(function(e) {
                    t.append(e.render().el)
                }), mixpanelTrack("Shop View", {
                    "Shop View Type": "Mobile Category"
                }), $("#categories-list", this.el).append(t)
            }
        })
    }), $(document).ready(function() {
        CategoryListView = Backbone.View.extend({
            tagName: "li",
            className: "selectable",
            events: {
                click: "select"
            },
            render: function() {
                return $(this.el).html("<span>" + this.model.get("name") + "</span>"), this
            },
            select: function() {
                _currentCategory = singularCategoryName(this.model.get("name")), _currentCategoryPlural = this.model.get("name"), Supreme.app.navigate("categories/" + this.model.get("name"), {
                    trigger: !0
                }), mixpanelTrack("Shop View", {
                    "Shop View Type": _currentCategory
                })
            }
        })
    }), $(document).ready(function() {
        CategoryProductsView = Backbone.View.extend({
            template: _.template(productListTemplate),
            initialize: function() {
                var e = this;
                this._productListViews = [], this.collection.each(function(t) {
                    e._productListViews.push(new ProductListView({
                        model: t
                    }))
                })
            },
            render: function() {
                var e = this;
                $(this.el).animate({
                    opacity: 0
                }, fadeSpeed, fadeEasingType, function() {
                    e.updateContent()
                })
            },
            updateContent: function() {
                var e = this;
                $(this.el).html(this.template), $("h2", this.el).text(this.model.get("name"));
                var t = $("<ul>");
                _(this._productListViews).each(function(e) {
                    t.append(e.render().el)
                }), $("ul", e.el).replaceWith(t), $(e.el).animate({
                    opacity: 1
                }, fadeSpeed, fadeEasingType)
            }
        })
    }), $(document).ready(function() {
        ChargeErrorViewTemplate = Backbone.View.extend({
            tagName: "div",
            template: _.template($("#chargeErrorTemplate").html()),
            render: function() {
                return $(this.el).html($(this.template(this.model))), this
            }
        })
    }), $(document).ready(function() {
        CheckoutView = Backbone.View.extend({
            template: _.template($("#checkoutViewTemplate").html()),
            initialize: function() {
                this.checkout_zipcodes = {}, IS_JAPAN || (_.bindAll(this, "countryChanged"), this.model.bind("change:order_billing_country", this.countryChanged)), IS_US && _.bindAll(this, "zipcodeAutofill"), _.bindAll(this, "creditCardTypeChanged"), this.model.bind("change:credit_card_type", this.creditCardTypeChanged), _.bindAll(this, "stateChanged"), this.model.bind("change:order_billing_state", this.stateChanged), this.model.bind("change:order_billing_zip", this.stateChanged), _.bindAll(this, "renderCheckoutErrors"), this.model.bind("checkoutErrors", this.renderCheckoutErrors), _.bindAll(this, "stateChanged"), _.bindAll(this, "renderDuplicateOrderError"), this.model.bind("dupError", this.renderDuplicateOrderError), _.bindAll(this, "renderOutOfStockError"), this.model.bind("outOfStockError", this.renderOutOfStockError), _.bindAll(this, "renderChargeError"), this.model.bind("chargeFailed", this.renderChargeError), this.bind("totalsRecalculationNeeded", this.updateTotals), IS_JAPAN ? this.rememberedFields = ["#order_billing_name", "#order_billing_last_name", "#order_email", "#order_tel", "#order_billing_address", "#order_billing_city", "#order_billing_state", "#order_billing_zip"] : this.rememberedFields = ["#order_billing_name", "#order_email", "#order_tel", "#order_billing_address", "#order_billing_address_2", "#order_billing_city", "#order_billing_state", "#order_billing_zip", "#order_billing_country"], IS_EU && this.rememberedFields.push("#order_billing_address_3")
            },
            renderChargeError: function(e) {
                $("#store_credits").remove(), $("#checkout-buttons").show(), e ? Supreme.app.navigate("#chargeErrorBot", {
                    trigger: !0
                }) : Supreme.app.navigate("#chargeError", {
                    trigger: !0
                })
            },
            populateAddressFromCookie: function() {
                if (_.isNull(readCookie("js-address"))) return;
                var e = decodeURIComponent(readCookie("js-address")).split("|");
                for (var t = 0; t < e.length; t++) $(this.rememberedFields[t], this.el).val(e[t]);
                this.model.set("order_billing_state", $("#order_billing_state", this.el).val()), this.model.set("order_billing_country", $("#order_billing_country", this.el).val())
            },
            render: function() {
                $(this.el).html(this.template());
                var e = this;
                $("#remember_address_label", this.el).click(function(e) {}), $("#order-terms-label", this.el).click(function(e) {}), $("#order-terms-label a", this.el).bind("click", function(e) {
                    var t = new StaticContentView;
                    t.render("terms"), window.scroll(0, 0), e.preventDefault()
                }), IS_JAPAN && ($("#order_billing_state", this.el).html($("#checkoutViewStatesTemplate").html()), Supreme.app.cart.hasItemsPreventingCOD() && $('#credit_card_type option[value="cod"]', this.el).remove()), IS_EU && (Supreme.app.cart.hasItemsPreventingNonEU() && ($('#order_billing_country option[value="BG"]', this.el).remove(), $('#order_billing_country option[value="HR"]', this.el).remove(), $('#order_billing_country option[value="EE"]', this.el).remove(), $('#order_billing_country option[value="FI"]', this.el).remove(), $('#order_billing_country option[value="GR"]', this.el).remove(), $('#order_billing_country option[value="LV"]', this.el).remove(), $('#order_billing_country option[value="LT"]', this.el).remove(), $('#order_billing_country option[value="NO"]', this.el).remove(), $('#order_billing_country option[value="RO"]', this.el).remove(), $('#order_billing_country option[value="SK"]', this.el).remove(), $('#order_billing_country option[value="SI"]', this.el).remove(), $('#order_billing_country option[value="SE"]', this.el).remove(), $('#order_billing_country option[value="CH"]', this.el).remove(), $('#order_billing_country option[value="IS"]', this.el).remove(), $('#order_billing_country option[value="RU"]', this.el).remove(), $('#order_billing_country option[value="BY"]', this.el).remove(), $('#order_billing_country option[value="TR"]', this.el).remove()), Supreme.app.cart.hasItemsPreventingRussia() && ($('#order_billing_country option[value="IS"]', this.el).remove(), $('#order_billing_country option[value="RU"]', this.el).remove())), window.navigator.standalone && ($("#remember_address", this.el).parent().parent().hide(), $("#remember_address", this.el).parent().parent().after("<br />"), $("form", this.el).append('<input type="hidden" name="is_from_mobile_standlone" value="1" />')), window.pushNotificationId && $("form", this.el).append('<input type="hidden" name="push_notification_id" value="' + window.pushNotificationId + '" />'), window.ClickedPushNotificationId && $("form", this.el).append('<input type="hidden" name="clicked_push_notification_id" value="' + window.ClickedPushNotificationId + '" />'), navigator.userAgent.match(/Android/i) && $("form", this.el).append('<input type="hidden" name="is_from_android" value="1" />'), navigator.userAgent.match(/SupremeAndroidApp/i) && $("form", this.el).append('<input type="hidden" name="is_from_android_app" value="1" />'), window.IOS_APP && $("form", this.el).append('<input type="hidden" name="is_from_ios_native" value="1" />'), $("input, select", this.el).bind("focus", this.fieldFocusChanged), $("input, select", this.el).bind("blur", this.fieldFocusChanged), $("#remember_address", this.el).bind("change", function() {
                    $(this).prop("checked") ? createCookie("remember_me", 1, 182) : eraseCookie("remember_me")
                });
                var t = Supreme.app.cart.itemTotal();
                $("#subtotal", this.el).text(formatCurrency(t)), IS_EU && $("#subtotal_eu", this.el).text(formatCurrency(GBPtoEuro(t), "âŹ", !0)), IS_JAPAN || this.countryChanged();
                if (IS_EU) {
                    var n = $("#order_billing_country", this.el).find("option[selected=selected]");
                    n.length > 0 && this.model.set("order_billing_country", $("#order_billing_country", this.el).val())
                }
                return this.populateAddressFromCookie(), this.trigger("totalsRecalculationNeeded"), _.isNull(readCookie("remember_me")) || $("#remember_address", this.el).attr("checked", "checked"), $("input, select", this.el).on("focus", function(e) {
                    $("table", this.el).find("label").removeClass("active"), $(this).closest("tr").find("label:not(#address_2_spaceholder)").addClass("active")
                }), IS_US && ($("#order_billing_zip", this.el).bind("keyup", this.zipcodeAutofill), $("#order_tel").mask("000-000-0000"), this.setupInlineValidation()), this.maskCreditCardField(), this
            },
            captcha: function() {
                var e = this;
                window.recaptchaCallback = function() {
                    e.submitFormAfterCaptcha()
                }, $("#g-recaptcha").length > 0 && (this.recaptcha_id = grecaptcha.render("g-recaptcha", {
                    sitekey: $("#g-recaptcha").data("sitekey"),
                    size: "invisible",
                    badge: "inline",
                    callback: "recaptchaCallback"
                }))
            },
            events: {
                "change input": "fieldChanged",
                "change select": "fieldChanged",
                "submit form": "submitForm",
                "click #cancel_checkout": "cancelCheckout"
            },
            zipcodeAutofill: function(e) {
                var t = this;
                if ($("#order_billing_country").val() == "USA") {
                    var n = $("#order_billing_zip").val(),
                        r = n;
                    r.length >= 4 && r.length > 4 && (r = r.substring(0, 4), $.ajax({
                        url: "//supreme-images.s3.amazonaws.com/us-zipcodes/" + r + ".js",
                        jsonpCallback: "w",
                        success: function(e) {
                            t.checkout_zipcodes[r] = e;
                            if (n.length == 5)
                                for (var i = 0; i < e.length; i++) e[i].zipcode == n && ($("#order_billing_city").val(e[i].city), $("#order_billing_state").val(e[i].state), t.trigger("totalsRecalculationNeeded"), $("#order_billing_state").selectric("refresh"))
                        },
                        dataType: "jsonp"
                    }))
                }
            },
            cancelCheckout: function(e) {
                if (window.IOS_APP_NATIVE) {
                    location.href = "supreme://index", e.preventDefault();
                    return
                }
                return !0
            },
            rememberAddress: function(e) {
                var t = _.map(e, function(e) {
                    return encodeURIComponent($(e).val())
                });
                createCookie("js-address", t.join("|"), 182)
            },
            renderDuplicateOrderError: function() {
                Supreme.app.navigate("cart", {
                    trigger: !0
                })
            },
            renderOutOfStockError: function() {
                window.SUPPORT_SUPREME_PROTOCOL ? window.location = "supreme://cart" : Supreme.app.navigate("cart", {
                    trigger: !0
                })
            },
            renderCheckoutErrors: function(e) {
                $("#mobile_checkout_form").data("credit_verified", 0), $("#store_credits").remove(), $("#checkout-buttons").show();
                var t = this,
                    n = new RegExp("Shipping|Type|Last|First|is not included in the list");
                $(".unhappy", this.el).removeClass("unhappy"), $("#checkout-errors", this.el).hide(), $("#credit-card-checkout-errors", this.el).hide(), $("p.error", this.el).removeClass("error"), $(".checkbox-container label", this.el).removeClass("error"), $("tr.error", this.el).removeClass("error");
                var r = [],
                    i = {};
                $(".msg", t.el).remove(), _.isUndefined(e.order) || (_.each(e.order, function(e, r) {
                    var s = e.join(", ");
                    s.match(n) || (i.hasOwnProperty(r) ? i[r].push(e) : i[r] = e, $("#order_" + r, t.el).closest("td").addClass("error unhappy"))
                }), IS_JAPAN && Object.keys(i).length > 0 && (Object.keys(i).length != 1 || !!_.isUndefined(i.terms)) && $("#checkout-errors", t.el).html(" ĺĽĺćźăăăăăăŻĺĽĺĺĺŽšăŤčŞ¤ăăăăăžăă<br />EăĄăźăŤă˘ăăŹăšăéťčŠąçŞĺˇăéľäžżçŞĺˇăŻĺč§čąć°ĺ­ă§ăč¨ĺĽăă ăăă").show());
                var s = {};
                _.isUndefined(e.credit_card) || _.each(e.credit_card, function(e, r) {
                    var i = e.join(", ");
                    if (!i.match(n)) {
                        s.hasOwnProperty(r) ? s[r].push(e) : s[r] = e;
                        var o = "#credit_card_" + r;
                        r == "verification_value" && (o = "#cvv-container input"), r == "number" && (o = "#credit_card_n"), $(o, t.el).closest("td").addClass("error")
                    }
                });
                var o = new RegExp("_", "g"),
                    u = new RegExp("billing", "g"),
                    a = new RegExp("^number", "g"),
                    f = new RegExp("year", "g"),
                    l = new RegExp("verification value", "g");
                _.each(i, function(e, n) {
                    var r = e.join(", "),
                        i = n.replace(o, " ");
                    i = i.replace(u, ""), n == "terms" ? $("#order-terms-label", t.el).append('<span class="msg">' + (IS_JAPAN ? "ĺŠç¨čŚç´ăŤĺćăăŚăă ăă" : i + " " + r) + "</span>") : IS_JAPAN ? $("#order_" + n, t.el).parent().parent().addClass("error") : IS_EU ? $("#order_" + n, t.el).parent().append('<span class="msg">' + r + "</span>") : $("#order_" + n, t.el).parent().append('<span class="msg">' + i + " " + r + "</span>")
                }), _.each(s, function(e, n) {
                    var i = e.join(", "),
                        s = n.replace(o, " ");
                    s = s.replace(u, ""), s = s.replace(f, "date"), s = s.replace(l, ""), s = s.replace(a, "");
                    var c;
                    n == "number" ? (c = $("#credit_card_n", t.el), r.push("ăŤăźăçŞĺˇăçĄĺšă§ăă")) : n == "type" ? c = $("#credit_card_type", t.el) : n == "verification_value" ? (r.push("CVVçŞĺˇăçĄĺšă§ăă"), c = $("#credit_card_cvv", t.el)) : n == "year" && (r.push("ćĺšćéĺăă§ăă"), c = $("#credit_card_month", t.el)), c && (IS_JAPAN ? c.closest("tr").find("td").addClass("error") : c.parent().append('<span class="msg">' + s + " " + i + "</span>"))
                }), r.length > 0 && IS_JAPAN && $("#credit-card-checkout-errors", this.el).text(r.join(" ")).show(), Object.keys(s).length > 0 && Object.keys(i).length == 0 && window.scrollTo(0, $("#credit-card-information-header", this.el).offset().top), Object.keys(s).length == 0 && Object.keys(i).length == 1 && i.hasOwnProperty("terms") && window.scrollTo(0, $("#order-terms-label", this.el).offset().top)
            },
            disableSubmitButton: function() {
                $("#submit_button").attr("disabled", "disabled").addClass("loading")
            },
            enableSubmitButton: function() {
                $("#submit_button").removeAttr("disabled").removeClass("loading")
            },
            hideKeyboard: function() {
                $("#hidden_cursor_capture", this.el).focus()
            },
            submitForm: function(e) {
                this.hideKeyboard();
                if ($("#g-recaptcha").length > 0 && !$("#g-recaptcha-response").val()) return grecaptcha.execute(this.recaptcha_id), !1;
                this.submitFormAfterCaptcha(), e.preventDefault()
            },
            cookieSubValue: function() {
                var e = JSON.parse(decodeURIComponent(readCookie("pure_cart")));
                return delete e.cookie, _.each(e, function(t, n) {
                    _.isNull(localStorage.getItem(n.toString())) ? delete e[n] : Supreme.app.cart.quantityForSize(n) < t && (e[n] = Supreme.app.cart.quantityForSize(n))
                }), encodeURIComponent(JSON.stringify(e))
            },
            submitFormAfterCaptcha: function(e) {
                window.pookyCallback && window.pookyCallback(), $("#cookie-sub").val(this.cookieSubValue());
                var t = this;
                if ($("#mobile_checkout_form").data("credit_verified") == "1") {
                    var n = $(this.el).offset();
                    $("#checkout-loading", this.el).css({
                        top: n.top,
                        left: n.left,
                        width: n.width,
                        height: n.height,
                        opacity: 0
                    }), $("#cart-link").hide(), t.hideKeyboard(), $("#checkout-loading", this.el).show(), $("#checkout-loading", this.el).animate({
                        opacity: 1
                    }, fadeSpeed, fadeEasingType, function() {
                        window.scrollTo(0, 0), window.pollOrderStatus = function(t) {
                            var n = function() {
                                $.ajax({
                                    type: "GET",
                                    url: "/checkout/" + t + "/status.json",
                                    xhrFields: {
                                        withCredentials: !0
                                    },
                                    success: function(t) {
                                        t["status"] == "queued" ? window.setTimeout(n, 3e3) : e(t)
                                    },
                                    error: function() {
                                        window.setTimeout(n, 9e3)
                                    }
                                })
                            };
                            window.setTimeout(n, 3e3)
                        };
                        var e = function(e) {
                            $("#cart-link")
                                .show();
                            var n = t.model.processFormResponse(e);
                            n && $("#checkout-loading", this.el).animate({
                                opacity: 0
                            }, fadeSpeed, fadeEasingType, function() {
                                $(this).hide()
                            }), t.enableSubmitButton()
                        };
                        $.ajax({
                            type: "POST",
                            url: $("#mobile_checkout_form").attr("action"),
                            data: $("#mobile_checkout_form").serializeArray(),
                            dataType: "json",
                            timeout: 2e4,
                            success: function(t) {
                                t.status == "queued" ? pollOrderStatus(t.slug) : e(t)
                            },
                            xhrFields: {
                                withCredentials: !0
                            },
                            error: function(e, n, r) {
                                $("#cart-link").show(), logError("submitForm ajax error", {
                                    statusText: e.statusText,
                                    status: e.status,
                                    errorType: n,
                                    error: r
                                }), t.model.trigger("chargeFailed"), t.enableSubmitButton()
                            }
                        })
                    })
                } else {
                    t.disableSubmitButton();
                    var r, i = String.fromCharCode;
                    for (var s in window) s.match(RegExp(i(106) + i(81))) && (r = !0);
                    $.ajax({
                        type: "GET",
                        url: "/store_credits/verify",
                        data: {
                            email: $("#order_email").val(),
                            c: r
                        },
                        dataType: "html",
                        success: function(e) {
                            $("#mobile_checkout_form").data("credit_verified", "1"), $("#store_credits").remove(), $("#checkout-buttons").hide(), $("#checkout-buttons").after($(e)), $("#store_credits").show(), $.scrollTo($("#store_credits").offset().top), $("#store_credit").click(function(e) {
                                $("#store_credit_id").val($("#store_credit").attr("store_credit_id")), $("#mobile_checkout_form").data("credit_verified", "1"), t.submitFormAfterCaptcha(), t.enableSubmitButton(), e.preventDefault(), e.stopImmediatePropagation()
                            }), $("#no_store_credit").click(function(e) {
                                $("#mobile_checkout_form").data("credit_verified", "1"), t.submitFormAfterCaptcha(), t.enableSubmitButton(), e.preventDefault(), e.stopImmediatePropagation()
                            })
                        },
                        error: function(e, n, r) {
                            $("#mobile_checkout_form").data("credit_verified", "1"), t.submitFormAfterCaptcha(), t.enableSubmitButton()
                        }
                    })
                }
            },
            fieldFocusChanged: function(e) {
                if ($(e.target).siblings("label").hasClass("error")) return;
                e.type == "focus" ? $(e.target).siblings("label").addClass("focused") : e.type == "blur" && $(e.target).siblings("label").removeClass("focused")
            },
            fieldChanged: function(e) {
                this.model.set(e.target.id, $(e.target).val()), this.rememberAddress(this.rememberedFields)
            },
            creditCardTypeChanged: function(e) {
                var t = $("#credit_card_type", this.el).val();
                t == "cod" ? ($("#credit_card_number_row", this.el).hide(), $("#exp_date_row", this.el).hide(), $("#cvv_row", this.el).hide(), $("#paypal_info").hide()) : t == "paypal" ? ($("#credit_card_number_row", this.el).hide(), $("#exp_date_row", this.el).hide(), $("#cvv_row", this.el).hide(), $("#paypal_info").show()) : ($("#paypal_info").hide(), $("#credit_card_number_row", this.el).show(), $("#exp_date_row", this.el).show(), $("#cvv_row", this.el).show(), this.maskCreditCardField()), this.trigger("totalsRecalculationNeeded")
            },
            creditCardTypeFromNumber: function(e) {
                return e = (e || "").replace(/[^\d]/g, ""), e.match(/^5[1-5]\d$/) ? "mastercard" : e.match(/^4\d/) || e.match(/^4\d/) ? "visa" : e.match(/^3[47]\d/) ? "american_express" : e.match(/^35(28|29|[3-8]\d)\d$/) ? "jcb" : "UNKNOWN"
            },
            maskCreditCardField: function() {
                var e = this,
                    t = {
                        autoclear: !1,
                        onKeyPress: function(t, n, r, i) {
                            var s = IS_US ? e.creditCardTypeFromNumber($("#credit_card_n").val()) : $("#credit_card_type", e.el).val(),
                                o = ["9999 999999 99999", "9999 9999 9999 9999"],
                                u = s == "american_express" ? o[0] : o[1];
                            $("#credit_card_n", e.el).mask(u, i)
                        }
                    };
                $("#credit_card_n", this.el).mask("9999 9999 9999 9999", t)
            },
            countryChanged: function(e) {
                if (this.model.get("order_billing_country") == "USA") {
                    $("label#state_label", this.el).text("state"), $("#order_billing_state", this.el).html($("#checkoutViewStatesTemplate").html()), $("#intl-shipping-info", this.el).hide();
                    var t = $("#checkout-errors", this.el).text(),
                        n = new RegExp("10-digit phone number");
                    t = t.replace(n, "10-digit us phone number"), $("#checkout-errors").text(t)
                } else {
                    $("label#state_label", this.el).text("province"), $("#order_billing_state", this.el).html($("#checkoutViewProvincesTemplate").html()), $("#intl-shipping-info", this.el).show();
                    var t = $("#checkout-errors", this.el).text(),
                        n = new RegExp("10-digit us phone number");
                    t = t.replace(n, "10-digit phone number"), $("#checkout-errors").text(t), IS_EU && (Supreme.app.cart.hasVat(this.model.get("order_billing_country")) ? $(".eu #checkout-form span.field").removeClass("no-vat") : $(".eu #checkout-form span.field").addClass("no-vat"))
                }
                this.trigger("totalsRecalculationNeeded")
            },
            setShippingTotal: function() {
                var e = Supreme.app.cart.shippingTotal(this.model.get("order_billing_country"), this.model.get("order_billing_state"));
                FREE_SHIPPING && e == 0 ? ($("#shipping", this.el).addClass("free-shipping").text(IS_JAPAN ? "çĄć" : "free shipping"), IS_EU && $("#shipping_eu", this.el).text("")) : ($("#shipping", this.el).removeClass("free-shipping").text(formatCurrency(e)), IS_EU && $("#shipping_eu", this.el).text(formatCurrency(GBPtoEuro(e), "âŹ", !0)))
            },
            setOrderTotal: function() {
                var e = Supreme.app.cart.orderTotal(this.model.get("credit_card_type"), this.model.get("order_billing_country"), this.model.get("order_billing_state"));
                $("#total", this.el).text(formatCurrency(e)), IS_EU && $("#total_eu", this.el).text(formatCurrency(GBPtoEuro(e), "âŹ", !0))
            },
            stateChanged: function() {
                this.trigger("totalsRecalculationNeeded")
            },
            setVATDiscount: function() {
                var e = Supreme.app.cart.vatDiscount(this.model.get("order_billing_country"), this.model.get("order_billing_state"));
                e == 0 ? ($("#vat-discount-container", this.el).hide(), $("#vat-discount-total", this.el).text("")) : ($("#vat-discount-container", this.el).show(), $("#vat-discount-total", this.el).text("-" + formatCurrency(e)), $("#vat-discount-total_eu", this.el).text("-" + formatCurrency(GBPtoEuro(e), "âŹ", !0)))
            },
            setCODTotal: function() {
                $("#credit_card_type", this.el).val() == "cod" ? ($("#cod", this.el).text(formatCurrency(Supreme.app.cart.codTotal("cod"))), $("#cod_row", this.el).show()) : ($("#cod_row", this.el).hide(), $("#cod", this.el).text(""))
            },
            setTaxTotal: function() {
                var e = Supreme.app.cart.taxTotal(this.model.get("order_billing_country"), this.model.get("order_billing_state"));
                e != 0 ? ($("#sales-tax-container", this.el).show(), $("#sales-tax-container #sales-tax-total", this.el).text(formatCurrency(e))) : ($("#sales-tax-container", this.el).hide(), $("#sales-tax-container #sales-tax-total", this.el).text(""))
            },
            updateTotals: function() {
                var e = this;
                IS_EU && $("#order_billing_country").length ? $.ajax({
                    type: "GET",
                    url: "/checkout/totals_mobile.js",
                    data: {
                        "order[billing_country]": $("#order_billing_country").val(),
                        "cookie-sub": this.cookieSubValue(),
                        mobile: !0
                    },
                    dataType: "html",
                    success: function(e) {
                        $("#totals_response").replaceWith(e)
                    },
                    error: function(t, n, r) {
                        e.setShippingTotal(), e.setOrderTotal(), e.setTaxTotal(), e.setVATDiscount()
                    }
                }) : IS_US && $("#order_billing_state").length ? $.ajax({
                    type: "GET",
                    url: "/checkout/totals_mobile.js",
                    data: {
                        "order[billing_country]": $("#order_billing_country").val(),
                        "cookie-sub": this.cookieSubValue(),
                        "order[billing_state]": $("#order_billing_state").val(),
                        "order[billing_zip]": $("#order_billing_zip").val(),
                        mobile: !0
                    },
                    dataType: "html",
                    success: function(t) {
                        $("#totals_response").replaceWith(t), e.watchSurchageInfo()
                    },
                    error: function(t, n, r) {
                        e.setShippingTotal(), e.setOrderTotal(), e.setTaxTotal(), e.setVATDiscount()
                    }
                }) : (this.setShippingTotal(), this.setOrderTotal(), this.setTaxTotal(), IS_EU && this.setVATDiscount(), IS_JAPAN && this.setCODTotal())
            },
            watchSurchageInfo: function() {
                $("#surchage_info").click(function(e) {
                    $("#surchage_info_tooltip").remove(), $("body").append('<div id="surchage_info_tooltip">Canadian Surcharge covers all Goods and Services Tax (GST), Harmonized Sales Tax (HST) as well as Duty and Brokerage.<br><br>Canadian customers will not incur any additional charges upon delivery.<br /><span style="display:block;text-align:center;font-weight:bold;margin:10px 0 5px 0;"><span style="border:1px solid #CCC;padding:2px 10px">OK</span></span></div>'), $("#surchage_info_tooltip").css({
                        position: "absolute",
                        top: $("#surchage_info").offset().top - 20
                    }).show(), setTimeout(function() {
                        $("#surchage_info_tooltip").css("opacity", 1), $("html").bind("click.surchargeClear", function() {
                            $("#surchage_info_tooltip").remove(), $("html").unbind("click.surchargeClear")
                        })
                    }, 10), e.preventDefault()
                })
            },
            setupInlineValidation: function() {
                $("form", this.el).isHappy({
                    selectorScope: this.el,
                    fields: {
                        "#order_billing_name": {
                            required: !0,
                            message: "first & last name required",
                            test: function(e) {
                                return FIRST_AND_LAST_NAME_FORMAT.test(e)
                            }
                        },
                        "#order_tel": {
                            required: !0,
                            message: "10 digit number required",
                            test: function(e) {
                                return TEL_FORMAT.test(e.replace(/-|\(|\)|\s/g, ""))
                            }
                        },
                        "#order_billing_zip": {
                            required: !0,
                            message: "",
                            test: function(e) {
                                return $("#order_billing_country").val() == "CANADA" ? CANADA_ZIP.test(e) : US_ZIP.test(e)
                            }
                        },
                        "#order_email": {
                            required: !0,
                            message: "invalid email",
                            test: function(e) {
                                return EMAIL_FORMAT.test(e)
                            }
                        },
                        "#order_billing_address": {
                            required: !0,
                            message: "required"
                        },
                        "#order_billing_city": {
                            required: !0,
                            message: "required"
                        },
                        "#order_terms": {
                            required: !0,
                            message: "terms must be accepted",
                            test: function(e) {
                                $("#order_terms", this.el).prop("checked")
                            },
                            errorPosition: {
                                placement: "append",
                                selector: "#order-terms-label"
                            }
                        },
                        "#order_billing_state": {
                            required: !0,
                            message: ""
                        },
                        "#credit_card_n": {
                            required: !0,
                            message: "required"
                        },
                        "#cvv-container input": {
                            required: !0,
                            message: ""
                        }
                    }
                })
            }
        })
    }), $(document).ready(function() {
        ConfirmationItemViewTemplate = Backbone.View.extend({
            tagName: "tr",
            template: _.template($("#confirmationItemViewTemplate").html()),
            events: {
                "click td.cart-image img": "jumpToProduct"
            },
            jumpToProduct: function(e) {
                Supreme.app.navigate("products/" + this.model.product_id + "/" + this.model.style_id, {
                    trigger: !0
                })
            },
            render: function() {
                return $(this.el).html($(this.template(this.model))), this
            }
        })
    }), $(document).ready(function() {
        LookbookItemView = Backbone.View.extend({
            template: _.template($("#lookbookItemViewTemplate").html()),
            events: {
                "click .lookbook-item": "zoom"
            },
            initialize: function() {
                var e = this;
                this._isZoomed = !1, e.navClose = function(t) {
                    t.preventDefault(), t.stopImmediatePropagation();
                    var n = Backbone.history.getFragment().replace("/zoom", "");
                    Supreme.app.navigate(n, {
                        trigger: !1,
                        replace: !0
                    }), e.killZoom()
                }, e.wrapperClose = function(t) {
                    var n = Backbone.history.getFragment().replace("/zoom", "");
                    Supreme.app.navigate(n, {
                        trigger: !1,
                        replace: !0
                    }), e.killZoom()
                }, e.handleHash = function(t) {
                    t.oldURL.indexOf("zoom") > -1 && t.newURL.indexOf("zoom") <= -1 && e.killZoom()
                }
            },
            render: function(e) {
                this.model.stripSomeMarkupFromCaption();
                var t = this.model.toJSON();
                return $(this.el).html(this.template(t)), this
            },
            zoom: function(e) {
                if (this._isZoomed) return !1;
                $("html").addClass("is-zoomed");
                if (_isLookbookSwiping) return;
                var t = this,
                    n = new Image;
                $(n).attr({
                    src: t.model.get("medium"),
                    width: "862",
                    height: "1248"
                }), $(n).css({
                    width: "862px",
                    height: "1248px"
                }), t.img = n;
                var r = $('<div id="lookbook-zoom-wrapper"><div id="lookbook-scroller"></div></div>');
                $("#lookbook-scroller", r).append($(n)), r.css("top", $(window).scrollTop() + "px"), iPhone && r.css({
                    height: window.screen.availHeight
                }), $("#container").before(r), r.css({
                    opacity: 1
                }), t.scroller = new IScroll("#lookbook-zoom-wrapper", {
                    zoom: !0,
                    tap: !0,
                    hideScrollbar: !0,
                    scrollX: !0,
                    scrollY: !0,
                    zoomMax: 2
                });
                if (!iPad) {
                    var i = e.offsetX / 335 * 862 - r.width() / 2,
                        s = e.offsetY / 485 * 1248 - r.height() / 2;
                    s <= 0 && (s = 0), s > r.height() && (s = r.height()), t.scroller.scrollTo(-i, -s, 0)
                }
                $(n).bind("load", function() {}).attr({
                    src: t.model.get("url"),
                    width: 862,
                    height: 1248
                }), android ? $(n).on("tap", t.wrapperClose) : $(n).on("click", t.wrapperClose), $(".supreme-navigation").on("click", t.navClose), $(window).on("hashchange", t.handleHash);
                var o = Backbone.history.getFragment() + "/zoom";
                Supreme.app.navigate(o, {
                    trigger: !1
                }), t._isZoomed = !0
            },
            killZoom: function() {
                $("#categories-link").off("click.lookbook-zoom");
                var e = this;
                if (!this._isZoomed) return;
                window.scroll(0, 0), this.scroller.destroy(), android ? $(e.img).off("tap", e.wrapperClose) : $(e.img).off("click", e.wrapperClose), $(".supreme-navigation").off("click", e.navClose), $(window).off("hashchange", e.handleHash), e._isZoomed = !1, $("html").removeClass("is-zoomed"), $("#lookbook-zoom-wrapper").animate({
                    opacity: 0
                }, fadeSpeed, fadeEasingType, function() {
                    $("#lookbook-zoom-wrapper").remove()
                })
            }
        })
    }), $(document).ready(function() {
        LookbookView = Backbone.View.extend({
            template: _.template($("#lookbookViewTemplate").html()),
            render: function(e) {
                $("footer").hide();
                var t = this;
                this.collection.fetch({
                    success: function() {
                        $(t.el).html(t.template), _.each(t.collection.models, function(e) {
                            var n = new LookbookItemView({
                                model: e,
                                collection: t.collection
                            });
                            $(".swipe-wrap", t.el).append(n.render().el)
                        }), setTimeout(function() {
                            $("footer").show()
                        }, 100), window.lookbookSwiper = Swipe(document.getElementById("lookbook-items"), {
                            continuous: !1,
                            transitionEnd: function(e, t) {
                                _isLookbookSwiping = !1
                            },
                            callback: function() {
                                _isLookbookSwiping = !0, t.updateCaption(), ga_track("pageview")
                            }
                        }), t.updateCaption()
                    }
                })
            },
            updateCaption: function() {
                $("#lookbook-pos", this.el).html(window.lookbookSwiper.getPos() + 1 + "/" + this.collection.length), $("#lookbook-title", this.el).html(this.collection.title), $("#lookbook-item-caption", this.el).html(this.collection.at(window.lookbookSwiper.getPos()).get("caption"))
            }
        })
    }), $(document).ready(function() {
        OrderConfirmationView = Backbone.View.extend({
            tagName: "div",
            template: _.template($("#orderConfirmationTemplate").html()),
            initialize: function() {
                $("html").removeClass("activate-back")
            },
            render: function() {
                $(this.el).html(this.template(this.model));
                var e = this.model.currency,
                    t = $("<tbody>");
                return _.each(this.model.purchases, function(n) {
                    n.currency = e;
                    var r = new ConfirmationItemViewTemplate({
                        model: n
                    });
                    t.append(r.render().el)
                }), $("table tbody", this.el).replaceWith(t), !_.isUndefined(this.model.cod) && this.model.cod && $("#cod_row", this.el).show(), !_.isUndefined(this.model.discount_total) && this.model.discount_total && $("#discount_row", this.el).show(), !_.isUndefined(this.model.manual_review) && this.model.manual_review ? ($("#manual_checkout_copy", this.el).show(), $("#standard_checkout_copy", this.el).hide(), $("#cart-items", this.el).hide(), $("#totals", this.el).hide()) : ($("#manual_checkout_copy", this.el).hide(), $("#standard_checkout_copy", this.el).show()), $("#join_mailinglist").change(function(e) {
                    var t = $(e.target).attr("checked") ? "subscribe" : "unsubscribe",
                        n = {
                            commit: t
                        };
                    IS_EU && (n.eu_order_mailing_list = 1);
                    var r = "http://" + document.domain + "/order_mailinglist";
                    $.post(r, n), e.preventDefault()
                }), this
            }
        })
    }), $(document).ready(function() {
        ProductDetailView = Backbone.View.extend({
            template: _.template($("#productDetailTemplate").html()),
            styleDetailTemplate: _.template(styleDetailTemplate),
            styleSelectorView: null,
            events: {
                "click #style-image-container img": "zoom",
                "touchstart #size-options-link": "sizeOptionsTap",
                "touchstart #qty-options-link": "qtyOptionsTap",
                "touchend #size-options-link": "sizeOptionsTapEnd",
                "touchend #qty-options-link": "qtyOptionsTapEnd",
                "change #size-options": "sizeOptionsChanged",
                "change #qty-options": "qtyOptionsChanged"
            },
            initialize: function() {
                var e = this;
                this.$window = $(window), this._isZoomed = !1, this.render = _.bind(this.render, this), this.renderWidgets = _.bind(this.renderWidgets, this), this.renderStyleDetail = _.bind(this.renderStyleDetail, this), this.renderStyleSpecifics = _.bind(this.renderStyleSpecifics, this), this.model.bind("change:initialPhotoIndex", this.renderWidgets), this.model.bind("change:initialPhotoIndex", this.renderStyleDetail), this.model.bind("change:selectedStyle", this.renderStyleSpecifics), Supreme.app.cart.bind("itemAdded", this.renderWidgets), this.navClose = function(e) {
                    var t = Backbone.history.getFragment().replace("/zoom", "");
                    Supreme.app.navigate(t, {
                        trigger: !1,
                        replace: !0
                    }), e.preventDefault(), e.stopImmediatePropagation()
                }, this.wrapperClose = function(e) {
                    var t = Backbone.history.getFragment().replace("/zoom", "");
                    Supreme.app.navigate(t, {
                        trigger: !1,
                        replace: !0
                    })
                }, this.handleHash = function(t) {
                    t.oldURL.indexOf("zoom") > -1 && t.newURL.indexOf("zoom") <= -1 && e.killZoom()
                }, this.$window.bind("kill-zoom", function() {
                    e.killZoom()
                }), this.$window.bind("start-zoom", function(t) {
                    e.zoom(t)
                }), this.model.get("styles").each(function(e) {
                    prefetchImage(e.get("swatch_url"))
                })
            },
            render: function() {
                $(this.el).empty();
                if (Backbone.history.fragment == "cart") return;
                var e = this.model.toJSON();
                return $(this.el).html(this.template(e)), this.renderStyleSpecifics(!0), $("footer").css({
                    position: "relative",
                    bottom: "auto",
                    left: "auto",
                    right: "auto"
                }), $("#keep-shopping", this.el).click(function(e) {
                    e.preventDefault(), Supreme.app.navigate("#categories", {
                        trigger: !0
                    })
                }), $("#itunes_link").click(function() {
                    window.open($(this).attr("href"), "itunes_store")
                }), mixpanelTrack("Product View", {
                    Category: _currentCategoryPlural,
                    "Product Color": e.selectedStyle.get("name"),
                    "Product Number": e.ino,
                    "Product Name": e.name,
                    "Product Cost": formatCurrency(this.model.actualPrice()),
                    "Device Type": "Mobile"
                }), this
            },
            renderStyleSpecifics: function(e) {
                this.renderStyleDetail(e), this.renderWidgets()
            },
            renderWidgets: function() {
                var e = Backbone.history.fragment.split("/");
                e = e[1];
                if (parseInt(e) != this.model.get("id")) return;
                var t = new ProductWidgetsView({
                    model: this.model
                });
                $("#product-widgets", this.el).html(t.render().el), this.addToCartButton = new AddToCartButtonView({
                    model: this.model
                }), this.sizeOptionsChanged(), this.qtyOptionsChanged(), this.model.get("selectedStyle").get("sizes").all(function(e) {
                    return e.get("stock_level") == 0
                }) && this.addToCartButton.soldOut();
                var n = (new StyleSelectorView({
                    el: $("#styles", this.el),
                    model: this.model
                })).render();
                this.model.has("selectedStyle") && this.highlightCurrent();
                var r = Supreme.app.cart.hasStyle(this.model.get("selectedStyle")),
                    i = Supreme.app.cart.canAddStyle(this.model.get("selectedStyle"));
                r ? ($("#in-cart", this.el).show(), $("#size-options-link", this.el).hide(), $("#qty-options-link", this.el).hide()) : i ? ($("#in-cart", this.el).hide(), this.model.get("selectedStyle").isSoldOut() ? ($("#size-options-link", this.el).hide(), $("#qty-options-link", this.el).hide()) : ($("#size-options-link", this.el).show(), $("#qty-options-link", this.el).show())) : ($("#qty-options-link", this.el).hide(), $("#cart-warning", this.el).remove(), this.model.get("can_buy_multiple_with_limit") > 1 ? $("#product-widgets", this.el).append($('<p id="cart-warning" class="warning"><span>' + LANG.limited_with_count + "</span></p>")) : $("#product-widgets", this.el).append($('<p id="cart-warning" class="warning"><span>' + LANG.limited + "</span></p>")), $("#widgets-container", this.el).hide()), this.model.get("cod_blocked") && $("#cod_blocked_product_view", this.el).show(), this.model.get("selectedStyle").get("sizes").models.length == 1 && this.model.get("selectedStyle").get("sizes").first().get("name") == "N/A" && $("#size-options-link", this.el).hide(), $("#cart-update", this.el).empty(), $("#cart-update", this.el).append(this.addToCartButton.render().el), $("#size-options option", this.el).length <= 1 ? $("#size-options-link", this.el).removeClass("enabled") : $("#size-options-link", this.el).addClass("enabled"), $("#qty-options option", this.el).length <= 1 ? $("#qty-options-link", this.el).removeClass("enabled") : $("#qty-options-link", this.el).addClass("enabled");
                var s = getNextProductFromId(this.model.get("id"));
                s.id == this.model.get("id") ? $("#next", this.el).hide() : ($("#next", this.el).show(), $("#next", this.el).attr("href", "#products/" + s.id), $("#next span", this.el).html(_currentCategory), $("#next", this.el).click(function(e) {
                    setTimeout(function() {
                        window.location.href = "#products/" + s.id
                    }, 140), $.scrollTo(0, 130), e.preventDefault()
                })), setTimeout(function() {
                    $("#product-widgets", this.el).css("opacity", 1), $("#product-details-content", this.el).css("opacity", 1), $("#product-nav", this.el).css("opacity", 1)
                }, 100)
            },
            highlightCurrent: function() {
                $("#style-selector li div img", this.el).removeClass("selected");
                var e = this.model.get("selectedStyle").get("initialPhotoIndex");
                e == 0 ? $("#style-" + this.model.get("selectedStyle").get("id") + " img.style-thumb", this.el).addClass("selected") : $("#style-" + this.model.get("selectedStyle").get("id") + ' img[altidx="' + e + '"]', this.el).addClass("selected")
            },
            renderStyleDetail: function(e) {
                var t = this;
                if (e === !0 || parseInt($("#product").attr("data-product-id")) != this.model.get("id")) {
                    $("#style-image", this.el).html(this.styleDetailTemplate(this.model.get("selectedStyle").toJSON())), $("#zoom-close").click(function(e) {
                        t.killZoom()
                    });
                    var n = "";
                    t.model.get("styles").each(function(e) {
                        var t = new Image;
                        $(t).attr({
                            width: iPad ? 600 : isHiResAndWide() ? 300 : 250,
                            height: iPad ? 600 : isHiResAndWide() ? 300 : 250
                        }).css({
                            visibility: "hidden"
                        }).bind("load", function() {
                            $(this).css({
                                visibility: "visible"
                            })
                        }), ($(t).attr("complete") || $(t).attr("complete") === undefined) && $(t).trigger("load");
                        var r = $('<div class="swiper-image-container">'),
                            i = $(t).attr("src", iPad ? e.zoomedPhoto() : e.productPhoto());
                        r.append(t), n += r[0].outerHTML
                    }), $("#style-image-container", t.el).find(".swipe-wrap").html(n)
                } else window.productSwiper.slide(t.model.get("styles").indexOf(t.model.get("selectedStyle")), 1);
                var r = t.model.get("styles").indexOf(t.model.get("selectedStyle")),
                    i = $("#style-image-container", t.el).find(".swipe-wrap > div").get(r).children[0],
                    s = $("#style-image-container", t.el).find(".swipe-wrap > div").get(r).children[0].src;
                return s != t.model.get("selectedStyle").productPhoto() && ($(i).unbind("load").css("visibility", "hidden"), $(i).bind("load", function() {
                    $(this).css({
                        visibility: "visible"
                    })
                }), iPad ? i.src = t.model.get("selectedStyle").zoomedPhoto() : i.src = t.model.get("selectedStyle").productPhoto(), ($(i).attr("complete") || $(i).attr("complete") === undefined) && $(i).trigger("load")), t.killZoom(), $("#style-name", this.el).html(t.model.get("selectedStyle").get("name")), $("#name", this.el).html(t.model.get("name")), $("#product", this.el).attr("data-product-id", t.model.get("id")), t.model.get("selectedStyle").get("description") && t.model.get("selectedStyle").get("description").length > 0 && $("#description").text(t.model.get("selectedStyle").get("description")), $("#style-image-container").bind("touchmove", function() {
                    _isSwiping = !0, setTimeout(function() {
                        _isSwiping = !1
                    }, 1e3)
                }), this.highlightCurrent(), this
            },
            sizeOptionsChanged: function() {
                var e = $("#size-options", this.el).val();
                e = $("#size-options option[value='" + e + "']", this.el).text(), $("#size-options-link", this.el).text(e)
            },
            qtyOptionsChanged: function() {
                var e = $("#qty-options", this.el).val();
                e = $("#qty-options option[value='" + e + "']", this.el).text(), $("#qty-options-link", this.el).text(e)
            },
            sizeOptionsTap: function(e) {
                e.preventDefault(), e.stopImmediatePropagation(), $("#size-options option").length > 1 && ($(this).addClass("touching"), setTimeout(function() {
                    openSelect("#size-options")
                }, 10))
            },
            qtyOptionsTap: function(e) {
                e.preventDefault(), e.stopImmediatePropagation(), $("#qty-options option").length > 1 && ($(this).addClass("touching"), setTimeout(function() {
                    openSelect("#qty-options")
                }, 10))
            },
            sizeOptionsTapEnd: function(e) {
                e.preventDefault(), e.stopImmediatePropagation(), $(this).removeClass("touching")
            },
            qtyOptionsTapEnd: function(e) {
                e.preventDefault(), e.stopImmediatePropagation(), $(this).removeClass("touching")
            },
            updateUrl: function() {
                var e = "products/" + this.model.get("id") + "/" + this.model.get("selectedStyle").get("id");
                Supreme.app.navigate(e, {
                    trigger: !1
                }), setLastVisitedFragment(e), _gaq.push(["_trackPageview", "/mobile/" + e])
            },
            zoom: function(e) {
                $("html").addClass("is-zoomed");
                var t = typeof e.type != "undefined" && e.type == "start-zoom";
                if (_isSwiping && !t) return;
                if (iPad) return;
                var n = this;
                _gaq.push(["_trackEvent", "products", "zoom", "Product ID " + n.model.get("id")]);
                var r = Backbone.history.fragment.split("/")[1];
                if (r != n.model.get("id")) return;
                n.finishZoomBits(e)
            },
            finishZoomBits: function(e) {
                var t = this,
                    n = new Image;
                $(n).attr({
                    src: t.model.get("selectedStyle").productPhoto(),
                    width: "600",
                    height: "600"
                }), $(n).css({
                    width: "600px",
                    height: "600px"
                }), window.navigator.standalone && $(n).addClass("big-screen"), $(window).height() >= 548 && $(n).addClass("app-screen");
                var r = 600;
                android ? $("#product-zoom-wrapper").on("tap", t.wrapperClose) : $("#product-zoom-wrapper").on("click", t.wrapperClose), $(".supreme-navigation").on("click.product-zoom", t.navClose), $("#product-zoom-scroller").html($(n)), $("#product-zoom-wrapper").show().animate({
                    opacity: 1
                }, fadeSpeed, fadeEasingType), scroller = new IScroll("#product-zoom-scroller", {
                    zoom: !0,
                    hideScrollbar: !0,
                    tap: !0,
                    click: !0,
                    scrollX: !0,
                    scrollY: !0,
                    zoomMax: 1.2,
                    zoomMin: .8,
                    startZoom: isHiResAndWide() ? 1 : .8,
                    directionLockThreshold: 20
                }), window.orientation == 90 || window.orientation == -90 ? window.navigator.standalone || $(window).width() >= 568 ? $(window).width() >= 736 ? scroller.scrollTo(-35, -45, 0) : $(window).width() >= 667 ? scroller.scrollTo(-60, -45, 0) : scroller.scrollTo(-130, -45, 0) : scroller.scrollTo(-90, -45, 0) : window.navigator.standalone || $(window).height() >= 548 ? $(window).width() >= 414 ? scroller.scrollTo(-115, -20, 0) : $(window).width() >= 375 ? scroller.scrollTo(-140, -40, 0) : scroller.scrollTo(-105, -9, 0) : scroller.scrollTo(-105, -25, 0);
                var i = 0,
                    s = !1;
                if (_.keys(_zoomedImageDownloadSpeeds).length > 3) {
                    var i = median(_.values(_zoomedImageDownloadSpeeds));
                    i > 1e3 && (s = !0)
                }
                var o = (new Date).getTime(),
                    u = t.model.get("selectedStyle").zoomedPhoto(s);
                $(n).bind("load", function() {
                    var e = (new Date).getTime();
                    if (_.isUndefined(_zoomedImageDownloadSpeeds[u])) {
                        _zoomedImageDownloadSpeeds[u] = e - o, _zoomedImageLoadOrder.push(u);
                        if (_zoomedImageLoadOrder.length > 5) {
                            var t = _zoomedImageLoadOrder.shift();
                            delete _zoomedImageDownloadSpeeds[t]
                        }
                    }
                }).attr({
                    src: u,
                    width: r,
                    height: r
                });
                var a = Backbone.history.getFragment() + "/zoom";
                Supreme.app.navigate(a, {
                    trigger: !1
                }), t.$window.on("hashchange", t.handleHash), t._isZoomed = !0
            },
            killZoom: function() {
                if (!this._isZoomed) return;
                $("html").removeClass("is-zoomed"), $(".supreme-navigation").off("click.product-zoom", this.navClose), android ? $("#product-zoom-wrapper").off("tap", this.wrapperClose) : $("#product-zoom-wrapper").off("click", this.wrapperClose), this.$window.off("hashchange", this.handleHash);
                var e = this;
                $("#product-zoom-wrapper").animate({
                    opacity: 0,
                    duration: fadeSpeed
                }), setTimeout(function() {
                    $("#product-zoom-wrapper").hide(), scroller.destroy(), $("#product-zoom-scroller").html(""), popZoomTriggered = !1, e._isZoomed = !1
                }, fadeSpeed - 500)
            }
        })
    });
var openSelect = function(e) {
    var t = $(e)[0],
        n = !1;
    if (document.createEvent) {
        var r = document.createEvent("MouseEvents");
        r.initMouseEvent("mousedown", !0, !0, window, 0, 0, 0, 0, 0, !1, !1, !1, !1, 0, null), n = t.dispatchEvent(r)
    } else t.fireEvent && (n = t.fireEvent("onmousedown"))
};
$(document).ready(function() {
    ProductListView = Backbone.View.extend({
        tagName: "li",
        className: "selectable",
        template: _.template(categoryProductListItemView),
        events: {
            click: "select"
        },
        render: function() {
            return $(this.el).html(this.template(this.model.toJSON())), this
        },
        select: function() {
            Supreme.app.navigate("products/" + this.model.get("id"), {
                trigger: !0
            })
        }
    })
}), $(document).ready(function() {
    ProductWidgetsView = Backbone.View.extend({
        template: _.template($("#productWidgetsTemplate").html()),
        tagName: "div",
        id: "widgets-container",
        initialize: function() {
            this.render = _.bind(this.render, this), Supreme.app.cart.bind("itemAdded", this.render)
        },
        render: function() {
            var e = this.model.toJSON(),
                t = Supreme.app.cart.getSizeForStyle(this.model.get("selectedStyle"));
            return _.extend(e, {
                sizeForStyleInCart: t
            }), $(this.el).html(this.template(e)), this
        }
    })
}), $(document).ready(function() {
    StaticContentView = Backbone.View.extend({
        template: _.template($("#staticViewTemplate").html()),
        initialize: function() {
            var e = this;
            e.closeWithBackButton = function(t) {
                e.close(), t.preventDefault(), t.stopImmediatePropagation()
            }, e.handleHash = function(t) {
                return e.close(), !1
            }, $(window).on("hashchange", e.handleHash), $(".supreme-navigation").on("click", e.closeWithBackButton)
        },
        close: function() {
            var e = this;
            $("html").removeClass("static-view"), $(window).off("hashchange", e.handleHash), $(".supreme-navigation").off("click", e.closeWithBackButton), $.scrollTo(0, scrollSpeed, function() {
                $("#static-view").animate({
                    opacity: 0
                }, fadeSpeed, fadeEasingType, function() {
                    $("#static-view").remove(), $("#static-loading").remove(), $("footer").css({
                        visibility: "visible"
                    })
                })
            })
        },
        render: function(e) {
            var t = this,
                n = "/mobile/static/" + e,
                r = $('<div id="static-loading"></div>');
            $("footer").css({
                visibility: "hidden"
            }), r.css({
                width: $("body").width(),
                height: $("body").height() + $("header").height(),
                position: "absolute",
                top: $("header").offset().top + $("header").height(),
                left: 0,
                background: "#F4F4F4",
                zIndex: 1999
            }), $("body").prepend(r), $.get(n, function(e) {
                var n = $(t.template());
                $("#static-content", n).html(e), $("#main").before(n), $("#static-view").animate({
                    opacity: 1
                }, fadeSpeed, fadeEasingType, function() {
                    r.bind("click", function() {
                        t.close()
                    }), $("#static-view p.close span").bind("click", function() {
                        t.close()
                    })
                })
            })
        }
    })
}), $(document).ready(function() {
    StyleSelectorOptionView = Backbone.View.extend({
        tagName: "li",
        template: _.template($("#styleSelectorOptionTemplate").html()),
        events: {
            "click img": "select"
        },
        render: function() {
            return $(this.el).html(this.template(this.model.toJSON())), this
        },
        select: function(e) {
            var t = $(e.target).parent().parent().attr("id").split("-")[1],
                n;
            $(e.target).hasClass("alternate") ? n = parseInt($(e.target).attr("altidx")) : n = 0;
            if (parseInt(t) != parseInt(this.model.get("product").get("selectedStyle").get("id"))) {
                var r = this.model.get("product").get("styles").get(t);
                r.set("initialPhotoIndex", n), this.model.get("product").set("selectedStyle", r)
            } else {
                this.model.get("product").get("selectedStyle").set("initialPhotoIndex", n);
                var i = (new Date).getTime();
                this.model.get("product").set("initialPhotoIndex", i)
            }
            var s = "products/" + this.model.get("product").get("id") + "/" + this.model.get("product").get("styles").get(t).get("id");
            Supreme.app.navigate(s, {
                trigger: !1
            }), setLastVisitedFragment(s), _gaq.push(["_trackPageview", "/mobile/" + s]);
            var o;
            IS_JAPAN ? o = "JPY" : IS_EU ? o = LANG.currency == "eur" ? "EUR" : "GBP" : o = "USD";
            var u = $(".price").text().replace(/[^\d\.]/, "");
            return mixpanelTrack("Product View", {
                Category: _currentCategoryPlural.toLocaleLowerCase(),
                "Product Color": this.model.get("product").get("selectedStyle").get("name"),
                "Product Name": this.model.get("product").get("name"),
                "Product Cost": u,
                Currency: o,
                "Device Type": "Mobile"
            }), !1
        }
    })
}), $(document).ready(function() {
    StyleSelectorView = Backbone.View.extend({
        template: _.template($("#styleSelectorTemplate").html()),
        initialize: function() {
            var e = this;
            this._styleSelectorOptionViews = [], this.model.get("styles").each(function(t) {
                e._styleSelectorOptionViews.push(new StyleSelectorOptionView({
                    model: t,
                    product: e.model,
                    id: "style-" + t.get("id")
                }))
            })
        },
        render: function() {
            var e = this;
            $(this.el).html(this.template(this.model.toJSON()));
            var t = $("#style-selector", e.el);
            _(this._styleSelectorOptionViews).each(function(e) {
                t.append(e.render().el)
            });
            var n = t.find("li");
            return n.length >= 8 && t.addClass("wide"), this
        }
    })
});
var ptr = !1;
$(document).ready(function() {
    function t() {
        setTimeout(function() {
            e.css("opacity") < 1 && e.animate({
                opacity: 1
            }, fadeSpeed, fadeEasingType)
        }, 50)
    }
    var e = $("#main");
    AppRouter = Backbone.Router.extend({
        routes: {
            "": defaultRoute(),
            categories: "categories",
            "categories/*name": "categoryProductList",
            "products/:id": "productDetail",
            "products/:id/*styleId": "productDetail",
            cart: "cart",
            checkout: "checkout",
            chargeError: "chargeError",
            chargeErrorBot: "chargeErrorBot",
            confirmOrder: "confirmOrder",
            paypalConfirmOrder: "paypalConfirmOrder",
            lookbook: "lookbook",
            "lookbook/zoom": "lookbook"
        },
        lookbook: function() {
            if (goLastPlace(Supreme.app)) return !1;
            if (Backbone.history.getFragment().indexOf("zoom") > -1) {
                var n = Backbone.history.getFragment().replace("/zoom", "");
                Supreme.app.navigate(n, {
                    trigger: !0,
                    replace: !0
                })
            }
            showCartAndCheckout(), e.animate({
                opacity: 0
            }, fadeSpeed, fadeEasingType, function() {
                window.scroll(0, 0), e.empty();
                var n = new Lookbook,
                    r = new LookbookView({
                        collection: n,
                        el: e[0]
                    });
                r.render(), _.delay(function() {
                    $("footer").animate({
                        opacity: 1
                    }, fadeSpeed, fadeEasingType), e.animate({
                        opacity: 1
                    }, fadeSpeed, fadeEasingType, t())
                }, 500)
            })
        },
        categories: function() {
            function n() {
                if (goLastPlace(Supreme.app)) return !1;
                showCartAndCheckout(), _currentViewHash.categories = JSON.stringify(Supreme.categories).hashCode(), Supreme.categories.each(function(e) {
                    for (var t = 0; t < e.get("products").length; t++) {
                        if (t >= 2) break;
                        prefetchImage(e.get("products").at(t).get("image_url"))
                    }
                });
                var n = new CategoryCollectionView({
                    collection: Supreme.categories,
                    el: e[0]
                });
                e.animate({
                    opacity: 0
                }, fadeSpeed, fadeEasingType, function() {
                    markItemTimeViewed("categories"), n.render(), e.animate({
                        opacity: 1
                    }, fadeSpeed, fadeEasingType, function() {
                        t(), !window.IOS_APP && !_.isUndefined(window.navigator.standalone) && !window.navigator.standalone && _.isNull(readCookie("hasVisited")) && createCookie("hasVisited", "1", 1e5)
                    })
                })
            }
            SHOP_CLOSED ? e.animate({
                opacity: 0
            }, fadeSpeed, fadeEasingType, function() {
                e.animate({
                    opacity: 1
                }, fadeSpeed, fadeEasingType, t()), e.html($("#shop-closed-content").html()), watchShopClosedForm(), observeFooterLinks()
            }) : Supreme.categories ? n() : setTimeout(function() {
                n()
            }, 750)
        },
        categoryProductList: function(t) {
            if (goLastPlace(Supreme.app)) return !1;
            showCartAndCheckout();
            var n = Supreme.categories.find(function(e) {
                return e.get("name") == t
            });
            _currentCategory || (_currentCategory = singularCategoryName(t), _currentCategoryPlural = t), n.get("products").each(function(e) {
                prefetchImage(e.get("image_url"))
            }), _currentViewHash.categoryProductList = JSON.stringify(n.get("products")).hashCode();
            var r = new CategoryProductsView({
                collection: n.get("products"),
                el: e[0],
                model: n
            });
            r.render(), markItemTimeViewed("categoryProductList")
        },
        productDetail: function(n, r) {
            if (Backbone.history.getFragment().indexOf("zoom") > -1) {
                var i = Backbone.history.getFragment().replace("/zoom", "");
                Supreme.app.navigate(i, {
                    trigger: !0,
                    replace: !0
                })
            }
            if (goLastPlace(Supreme.app)) return !1;
            showCartAndCheckout(), $("footer").hide();
            var s = Supreme.getProductOverviewDetailsForId(n, allCategoriesAndProducts);
            if (_.isUndefined(s)) {
                Supreme.app.navigate("#", {
                    trigger: !0
                });
                return
            }
            var o = new Product({
                id: n,
                name: s.name,
                price: s.price,
                price_euro: s.price_euro,
                sale_price: s.sale_price,
                sale_price_euro: s.sale_price_euro,
                categoryName: s.categoryName
            });
            _currentViewHash.product = JSON.stringify(s).hashCode(), _currentCategory || (_currentCategory = singularCategoryName(s.categoryName), _currentCategoryPlural = s.categoryName);
            var u = new TrackTiming("Mobile application", "Show product detail");
            e.animate({
                opacity: 0
            }, fadeSpeed, fadeEasingType, function() {
                window.scrollTo(0, 0), e.animate({
                    opacity: 1
                }, fadeSpeed, fadeEasingType, t()), $("footer").show();
                var i = '<div id="product"><h2 id="name">' + s.name + '</h2><p id="style-name">&nbsp;</p><div id="style-image"><div id="style-image-container" class="swipe loading" style="visibility: visible; "></div><div class="clearfix"></div></div></div><div id="product-widgets" class="clearfix  "><div id="widgets-container"><span class="price">&nbsp;</span><span id="cart-update"></span></div></div><div style="margin-bottom:' + $(window).height() + 'px;" id="product-details"><div id="product-details-content"><div id="styles"><ul id="style-selector"><li><div class="style-images"></div></li></ul><div class="clearfix"></div></div><p style="height:150px;" id="description">&nbsp;</p></div></div>';
                e.html(i), o.fetch({
                    success: function(t) {
                        markItemTimeViewed(n), _.isUndefined(r) ? o.set("selectedStyle", o.get("styles").first()) : o.set("selectedStyle", o.get("styles").get(r)), o.get("styles").each(function(e) {
                            e.get("sizes").each(function(e) {
                                e.get("stock_level") > 0 && !_.isNull(sessionStorage.getItem("out_of_stock_" + e.get("id"))) && sessionStorage.removeItem("out_of_stock_" + e.get("id"))
                            })
                        }), productDetailView = new ProductDetailView({
                            model: o
                        }), window.clearInterval(productTimer), productTimer = setInterval(function() {
                            productDetailViewPoller(n)
                        }, _productPollInterval), e.html(productDetailView.render().el), $("#style-image-container").css("opacity", 0), window.productSwiper = Swipe(document.getElementById("style-image-container"), {
                            speed: 200,
                            startSlide: productDetailView.model.get("styles").indexOf(productDetailView.model.get("selectedStyle")),
                            transitionEnd: function(e, t) {
                                _isSwiping = !1;
                                var n = productDetailView.model.get("styles").at(e);
                                productDetailView.model.set("selectedStyle", n), productDetailView.updateUrl()
                            },
                            continuous: !1,
                            disableSwipe: productDetailView.model.get("styles").length == 1
                        }), $("#style-image-container").css({
                            opacity: 1
                        }), o.get("styles").each(function(e) {
                            iPad ? prefetchImage(e.zoomedPhoto()) : prefetchImage(e.productPhoto())
                        }), u.endTime().send()
                    }
                })
            })
        },
        cart: function() {
            if (goLastPlace(Supreme.app)) return !1;
            var n = new CartView({
                model: Supreme.app.cart
            });
            e.animate({
                opacity: 0
            }, fadeSpeed, fadeEasingType, function() {
                Supreme.app.cart.fetch({
                    success: function(r) {
                        n.render(), Supreme.app.cart.lastError = "", e.animate({
                            opacity: 1
                        }, fadeSpeed, fadeEasingType, t()), Backbone.history.fragment == "cart" && ($("footer").css("opacity", 0), $("#goto-cart-link").hide(), $("#checkout-now").show())
                    }
                })
            })
        },
        checkout: function() {
            if (goLastPlace(Supreme.app)) return !1;
            if (!$("body").hasClass("for-native-checkout") && Supreme.app.cart.length() == 0) {
                Supreme.app.navigate("#", {
                    trigger: !0
                });
                return
            }
            e.animate({
                opacity: 0
            }, fadeSpeed, fadeEasingType, function() {
                var n = new CheckoutForm,
                    r = new CheckoutView({
                        model: n
                    });
                $.scrollTo(0), e.html(r.render().el), $("select").selectric(), r.captcha(), IS_EU && r.countryChanged(), IS_US && r.stateChanged(), e.animate({
                    opacity: 1
                }, fadeSpeed, fadeEasingType, t()), $("footer").css("opacity", 1), Backbone.history.fragment == "checkout" && ($("#goto-cart-link").show().text("EDIT CART").addClass("edit"), $("#checkout-now").hide())
            })
        },
        chargeError: function(t) {
            var n = {};
            t ? n.botError = !0 : n.botError = !1, showCartAndCheckout();
            if (goLastPlace(Supreme.app)) return !1;
            var r = new ChargeErrorViewTemplate({
                el: e[0],
                model: n
            });
            r.render()
        },
        chargeErrorBot: function() {
            this.chargeError(!0)
        },
        paypalConfirmOrder: function() {
            showCartAndCheckout();
            if (goLastPlace(Supreme.app)) return !1;
            var t = {};
            document.location.search.replace(/\??(?:([^=]+)=([^&]*)&?)/g, function() {
                function e(e) {
                    return decodeURIComponent(e.split("+").join(" "))
                }
                t[e(arguments[1])] = e(arguments[2])
            }), localStorage.clear(), $("#cart-link").remove(), eraseCookie("cart"), Supreme.app.cart = new Cart, $.getJSON("/mobile/paypal_confirmation.json?t=" + t.t + "&i=" + t.i, function(t) {
                order = t.info;
                var n = new OrderConfirmationView({
                    el: e[0],
                    model: order
                });
                n.render(), $("html").addClass("orderConfirm")
            }), clearCookies()
        },
        confirmOrder: function() {
            showCartAndCheckout();
            if (goLastPlace(Supreme.app)) return !1;
            var t = {
                    id: 111,
                    billing_name: "First Last",
                    email: "example@email.com",
                    purchases: [{
                        image: "//d17ol771963kd3.cloudfront.net/142396/ca/Bennk6ztM1w.jpg",
                        product_name: "111",
                        style_name: "&nbsp;",
                        size_name: "N/A",
                        price: 111,
                        product_id: 111,
                        style_id: 111,
                        quantity: 1
                    }],
                    item_total: 4800,
                    shipping_total: 500,
                    tax_total: 0,
                    total: 5300,
                    currency: "ÂŁ",
                    store_credit: 0,
                    discount_total: 0,
                    created_at: "Feb 20 at 20:05",
                    vat_discount: 0
                },
                n = new OrderConfirmationView({
                    el: e[0],
                    model: t
                });
            n.render()
        }
    }), ptr || setTimeout(function() {
        console.log("Main: ", $("#main").length), ptr = PullToRefresh.init({
            shouldPullToRefresh: function() {
                return $("#products").length == 0 && $("#categories-list").length == 0 ? !1 : $(window).scrollTop() > 50 ? !1 : !0
            },
            onRefresh: function(e) {
                window._gaq.push(["_trackEvent", "pull-to-refresh", "pulled", Backbone.history.getFragment()]), loadDataForPoll(function() {
                    e()
                })
            }
        })
    }, 200)
}), String.prototype.hashCode = function() {
    var e = 0,
        t, n;
    if (this.length == 0) return e;
    for (t = 0, l = this.length; t < l; t++) n = this.charCodeAt(t), e = (e << 5) - e + n, e |= 0;
    return e
};
var Supreme, iPad = navigator.userAgent.match(/iPad/i) != null,
    iPhone = navigator.userAgent.match(/iphone|ipod/i) != null,
    android = navigator.userAgent.match(/Android/i) != null,
    _isSwiping = !1,
    _isLookbookSwiping = !1,
    _staleDataAge = 4e3,
    _pollInterval = 15e3,
    _productPollInterval = 15e3,
    _currentCategory = !1,
    _currentCategoryPlural = !1,
    _firstLaunched = !0,
    popZoomTriggered = !1,
    _zoomedImageDownloadSpeeds = {},
    _zoomedImageLoadOrder = [],
    _currentViewHash = {
        categories: "",
        categoryProductList: "",
        product: ""
    };
window.setPushNotificationId = function(e) {
    window.pushNotificationId = e, setTimeout(function() {
        window._gaq && window._gaq.push(["_trackEvent", "push-notifications", "received", e])
    }, 100)
}, window.setClickedPushNotificationId = function(e) {
    window.ClickedPushNotificationId = e, setTimeout(function() {
        ga && ga("send", "event", "push-notifications", "clicked", e)
    }, 100)
}, window.appEnteredForeground = function() {
    window.ClickedPushNotificationId && window.ClickedPushNotificationId.match(/^u:/) && (mixpanelTrack("App Open"), setTimeout(function() {
        var e = window.ClickedPushNotificationId.replace(/^u:/, "");
        window.ClickedPushNotificationId = null, Supreme.app.navigate(e, {
            trigger: !0
        })
    }, 100)), loadDataForPoll();
    var e = currentRoute();
    if (e.route == "productDetail" && !_.isUndefined(e.params) && e.params.length > 0) {
        var t = e.params[0];
        productDetailViewPoller(t)
    }
}, window.goToDeepLink = function(e) {
    setTimeout(function() {
        alert(e)
    }, 100)
}, TrackTiming.prototype.startTime = function() {
    return this.startTime = (new Date).getTime(), this
}, TrackTiming.prototype.endTime = function() {
    return this.endTime = (new Date).getTime(), this
}, TrackTiming.prototype.send = function() {
    var e = this.endTime - this.startTime;
    return window._gaq.push(["_trackTiming", this.category, this.variable, e, this.label]), this
};
var load_page_tt = new TrackTiming("Mobile application", "Initial loading");
load_page_tt.startTime(),
    function() {
        var e = document.createElement("script");
        e.type = "text/javascript", e.async = !0, e.src = ("https:" == document.location.protocol ? "https://ssl" : "http://www") + ".google-analytics.com/ga.js";
        var t = document.getElementsByTagName("script")[0];
        t.parentNode.insertBefore(e, t)
    }(), _.templateSettings = {
        interpolate: /\{\{(.+?)\}\}/g,
        evaluate: /\{\[([\s\S]+?)\]\}/g
    };
var soldOutMessage = IS_JAPAN ? "ăŤăźăĺăŤăăĺĺă§ĺŽĺŁ˛ăăŚăăžăŁăĺĺăăăăžăăăćł¨ććçśăăŤé˛ăĺăŤăŤăźăăăĺé¤ăăŚăă ăăă" : "Some of the items in your cart are now sold out. Remove them before check out.",
    productDetailView, scroller, categoryListTemplate = '<div id="categories-list" class="section"></div>',
    productListTemplate = '<div id="products" class="section"><h2></h2><ul></ul></div>',
    categoryProductListItemViewSize = isHiRes() ? 50 : 45,
    categoryProductListItemView = '<div class="clearfix"><img height="' + categoryProductListItemViewSize + '" width="' + categoryProductListItemViewSize + '" src="{{ image_url }}"><span class="info"><span class="name">{{ name }}</span>{[if(new_item && !this.model.isOnSale()){ ]} <span class="new">new</span>{[ } ]}{[if(this.model.isOnSale()){ ]} <span class="sale">sale</span>{[ } ]}</span></div>',
    styleDetailTemplate = '<div id="style-image-container" class="swipe"><div class="swipe-wrap"></div></div><div class="clearfix"></div>',
    scrollSpeed = 300,
    fadeSpeed = 400,
    fadeEasingType = "ease-out";
$(document).ready(function() {
    setHiResClass(), $(window).resize(function() {
        setHiResClass()
    }), Supreme = {
        getProductOverviewDetailsForId: function(e, t) {
            var n;
            return _.each(t.products_and_categories, function(t, r) {
                var i = _.find(t, function(t) {
                    return t.id == e
                });
                if (!_.isUndefined(i)) {
                    n = i, n.categoryName = r;
                    return
                }
            }), n
        }
    }, iPad && $("body").addClass("ipad"), navigator.userAgent.match(/Supreme-native\/(.*?) /i) || $("body").addClass("not-native-app"), (iPad || iPhone) && $("body").addClass("idevice"), FastClick.attach(document.body), window.screen.height == 568 && (document.querySelector("meta[name=viewport]").content = "width=320.1, initial-scale=1.0, maximum-scale=1.0, user-scalable=no"), observeFooterLinks();
    var e = readCookie("shoppingSessionId");
    _.isNull(e) ? (localStorage.clear(), setSessionIDs()) : parseInt(localStorage.getItem("shoppingSessionId")) != parseInt(e) && (localStorage.clear(), eraseCookie("shoppingSessionId"), setSessionIDs());
    var t;
    (t = location.search.match(/utm_medium=([a-z]+)/)) && createCookie("origin", t[1], 1), document.referrer.match(/facebook.com\//) && createCookie("origin", "facebook", 1), Supreme.categories = new Categories, allCategoriesAndProducts ? Supreme.categories.populate(allCategoriesAndProducts) : (loadDataForPoll(), setTimeout(Supreme.categories.populate(allCategoriesAndProducts), 250)), allCategoriesAndProducts.sale && $("#discount_banner").length == 0 && $("#main").before('<p id="discount_banner">' + allCategoriesAndProducts.sale_blurb + "</p>"), $(".supreme-navigation").on("click", function() {
        var e = $("html");
        if (e.hasClass("activate-back") && !e.hasClass("is-zoomed") && !e.hasClass("static-view")) {
            var t = Backbone.history.fragment.split("/");
            return Backbone.history.fragment == "lookbook" ? Supreme.app.navigate("categories", {
                trigger: !0
            }) : t[0] === "categories" ? Supreme.app.navigate("categories", {
                trigger: !0
            }) : t[0] === "products" ? Supreme.app.navigate("categories/" + _currentCategoryPlural, {
                trigger: !0
            }) : window.history.back(), !1
        }
    }), setInterval(function() {
        if (SHOP_CLOSED) return;
        if (!_.isUndefined(currentRoute()) && !_.isUndefined(currentRoute().route)) {
            var e = currentRoute();
            if (e.route == "categories" && itemViewIsStale("categories")) markItemTimeViewed("categories"), JSON.stringify(Supreme.categories).hashCode() != _currentViewHash.categories && Supreme.app.categories();
            else if (e.route == "categoryProductList" && itemViewIsStale("categoryProductList")) {
                markItemTimeViewed("categoryProductList");
                var t = Supreme.categories.find(function(e) {
                    return e.get("name") == currentRoute().params[0]
                });
                JSON.stringify(t.get("products")).hashCode() != _currentViewHash.categoryProductList && Supreme.app.categoryProductList(e.params[0])
            } else if (e.route == "productDetail" && !_.isUndefined(e.params) && e.params.length > 0) {
                var n = e.params[0];
                itemViewIsStale(n) && markItemTimeViewed(n)
            }
        }
    }, 500), setInterval(function() {
        loadDataForPoll()
    }, _pollInterval), Supreme.app = new AppRouter, Supreme.app.cart = new Cart, _.isNull(sessionStorage.getItem("hasVisited")) && (Supreme.app.cart.length() == 0 && clearCookies(), sessionStorage.setItem("hasVisited", 1)), Supreme.app.cartLinkView = new CartLinkView({
        model: Supreme.app.cart
    }), $("header").prepend(Supreme.app.cartLinkView.render().el), Backbone.history.bind("route", function(e, t, n) {
        setLastVisitedFragment(), _gaq.push(["_trackPageview", "/mobile/" + Backbone.history.fragment]);
        var r = Backbone.history.fragment.split("/");
        Backbone.history.fragment === "categories" && r[0] === "categories" || !Backbone.history.fragment ? $("html").attr("class", Backbone.history.fragment + " " + r[0] + " index-route") : $("html").attr("class", Backbone.history.fragment + " " + r[0] + " activate-back"), Backbone.history.fragment == "" && $("html").addClass("categories"), SHOP_CLOSED && ($("#shop_closed p.copy").show(), $("#email_field").show(), $("#eu_field").show(), $(".checkbox-container").show())
    }), Backbone.history.start(), setCurrentLangToggle(currentLang()), $("#intro-loading").animate({
        opacity: 0
    }, fadeSpeed, fadeEasingType, function() {
        $(this).remove(), fadeSpeed = 100
    }), _.delay(function() {
        $("footer").addClass("first-loaded").animate({
            opacity: 1
        }, fadeSpeed, fadeEasingType)
    }, 700), load_page_tt.endTime().send(), $("#current-lang").on("touchstart", function(e) {
        showLanguageSetter(), e.preventDefault()
    })
}), !_.isUndefined(window.navigator.standalone) && window.navigator.standalone && _gaq.push(["_trackEvent", "isStandalone", "launched"]), window.IOS_APP && function() {
    var e = window.XMLHttpRequest.prototype.open,
        t = window.XMLHttpRequest.prototype.send,
        n = "ajax",
        r, i, s, o;
    n = "ajax", ios = function(e, t, s) {
        var o = {
            url: r,
            method: i,
            data: s
        };
        t !== null && (o.status = t), window.location = n + "://" + e + "?xhr=" + JSON.stringify(o)
    }, window.XMLHttpRequest.prototype.open = function(t, n) {
        return i = t, r = n, e.apply(this, arguments)
    }, newOnReadyStateChange = function(e) {
        return this.readyState === 4 && (this.status == 200 || this.status == 304 ? ios("completed", this.status, "") : this.status > 500 && (ios("error", this.status, this.statusText), error = {
            url: r,
            status: this.status,
            statusText: this.statusText
        }, $.ajax({
            type: "GET",
            url: "http://app.supremenewyork.com/mobile_error",
            dataType: "jsonp",
            data: error
        }))), this._onreadystatechange.apply(this, arguments)
    }, window.XMLHttpRequest.prototype.send = function(e) {
        return this.onreadystatechange.__mine != null && this.onreadystatechange.__updated == null ? this.onreadystatechange = newOnReadyStateChange : this.onreadystatechange.__updated == null && (this._onreadystatechange = this.onreadystatechange, this.onreadystatechange = newOnReadyStateChange), ios("send", null, e), t.apply(this, arguments)
    }, newOnReadyStateChange.__updated = !0, newOnReadyStateChange.__mine = !0
}();